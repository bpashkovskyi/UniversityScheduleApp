using System.Text.Json;
using Microsoft.EntityFrameworkCore;
using UniversityScheduleApp.Data.Models;
using UniversityScheduleApp.Data.Responses.GroupApiResponse;
using UniversityScheduleApp.Data.Responses.RoomApiResponse;
using UniversityScheduleApp.Data.Responses.TeacherApiResponse;
using UniversityScheduleApp.Data.Responses.LessonApiResponse;

namespace UniversityScheduleApp.Data.Services
{
    public sealed class ApiService
    {
        private readonly HttpClient _httpClient;
        private readonly UniversityContext _context;

        public ApiService(HttpClient httpClient, UniversityContext context)
        {
            _httpClient = httpClient;
            _context = context;
        }

        public async Task FetchAndSaveRoomsAsync()
        {
            var response = await _httpClient.GetStringAsync(
                "https://dekanat.nung.edu.ua/cgi-bin/timetable_export.cgi?req_type=obj_list&req_mode=room&show_ID=yes&req_format=json&coding_mode=WINDOWS-1251&bs=ok");
            var data = JsonSerializer.Deserialize<RoomApiResponse>(response);

            foreach (var blockData in data.PsRozkladExport.Blocks)
            {
                // Create or find block
                var block = await _context.Blocks
                    .FirstOrDefaultAsync(b => b.Name == blockData.Name);

                if (block is null)
                {
                    block = new Block { Name = blockData.Name };
                    _context.Blocks.Add(block);
                    await _context.SaveChangesAsync(); // Save to get the ID
                }

                // Add rooms to the block
                foreach (var roomData in blockData.Objects)
                {
                    var room = new Room
                    {
                        Id = int.Parse(roomData.Id),
                        Name = roomData.Name,
                        BlockId = block.Id
                    };
                    _context.Rooms.Add(room);
                }
            }

            await _context.SaveChangesAsync();
        }

        public async Task FetchAndSaveTeachersAsync()
        {
            var response = await _httpClient.GetStringAsync(
                "https://dekanat.nung.edu.ua/cgi-bin/timetable_export.cgi?req_type=obj_list&req_mode=teacher&show_ID=yes&req_format=json&coding_mode=WINDOWS-1251&bs=ok");
            var data = JsonSerializer.Deserialize<TeacherApiResponse>(response);

            foreach (var departmentData in data.PsRozkladExport.Departments)
            {
                // Create or find teacher department
                var teacherDepartment = await _context.TeacherDepartments
                    .FirstOrDefaultAsync(td => td.Name == departmentData.Name);

                if (teacherDepartment is null)
                {
                    teacherDepartment = new TeacherDepartment { Name = departmentData.Name };
                    _context.TeacherDepartments.Add(teacherDepartment);
                    await _context.SaveChangesAsync(); // Save to get the ID
                }

                // Add teachers to the department
                foreach (var teacherData in departmentData.Objects)
                {
                    var teacher = new Teacher 
                    { 
                        Id = int.Parse(teacherData.Id), 
                        Name = teacherData.Name,
                        Surname = teacherData.Surname,
                        FirstName = teacherData.FirstName,
                        Patronymic = teacherData.Patronymic,
                        TeacherDepartmentId = teacherDepartment.Id
                    };
                    _context.Teachers.Add(teacher);
                }
            }

            await _context.SaveChangesAsync();
        }

        public async Task FetchAndSaveGroupsAsync()
        {
            var response = await _httpClient.GetStringAsync(
                "https://dekanat.nung.edu.ua/cgi-bin/timetable_export.cgi?req_type=obj_list&req_mode=group&show_ID=yes&req_format=json&coding_mode=WINDOWS-1251&bs=ok");
            var data = JsonSerializer.Deserialize<GroupApiResponse>(response);

            foreach (var departmentData in data.PsRozkladExport.Departments)
            {
                // Create or find group department
                var groupDepartment = await _context.GroupDepartments
                    .FirstOrDefaultAsync(gd => gd.Name == departmentData.Name);

                if (groupDepartment is null)
                {
                    groupDepartment = new GroupDepartment { Name = departmentData.Name };
                    _context.GroupDepartments.Add(groupDepartment);
                    await _context.SaveChangesAsync(); // Save to get the ID
                }

                // Add groups to the department
                foreach (var groupData in departmentData.Objects)
                {
                    var group = new Group
                    {
                        Id = int.Parse(groupData.Id),
                        Name = groupData.Name,
                        GroupDepartmentId = groupDepartment.Id
                    };
                    _context.Groups.Add(group);
                }
            }

            await _context.SaveChangesAsync();
        }

        public async Task FetchAndSaveLessonsAsync()
        {
            var startDate = "01.09.2025";
            var endDate = "31.12.2025";

            // Get all groups, teachers, and rooms from database
            var groups = await _context.Groups.ToListAsync();
            var teachers = await _context.Teachers.ToListAsync();
            var rooms = await _context.Rooms.ToListAsync();

            // Create lookup dictionaries for efficient matching
            var groupLookup = groups.ToDictionary(g => g.Name, g => g);
            var teacherLookup = teachers.ToDictionary(t => t.Name, t => t);
            var roomLookup = rooms.ToDictionary(r => r.Name, r => r);

            // Fetch lessons ONLY from groups (since description contains teacher and room info)
            foreach (var group in groups)
            {
                try
                {
                    // Get the next available lesson ID for this group
                    var maxLessonId = await _context.Lessons.MaxAsync(l => (int?)l.Id) ?? 0;
                    var lessonId = maxLessonId + 1;

                    var url =
                        $"https://dekanat.nung.edu.ua/cgi-bin/timetable_export.cgi?req_type=rozklad&req_mode=group&OBJ_ID={group.Id}&OBJ_name=&dep_name=&ros_text=separated&begin_date={startDate}&end_date={endDate}&req_format=json&coding_mode=WINDOWS-1251&bs=ok";
                    var response = await _httpClient.GetStringAsync(url);
                    var data = JsonSerializer.Deserialize<LessonApiResponse>(response);

                    lessonId = await ProcessLessonsAsync(data.PsRozkladExport.RozItems, groupLookup, teacherLookup,
                        roomLookup, lessonId);

                    // Save changes after each group for better performance and progress tracking
                    await _context.SaveChangesAsync();
                    Console.WriteLine($"✓ Processed lessons for group {group.Name}");
                }
                catch (Exception ex)
                {
                    // Log error but continue with other groups
                    Console.WriteLine($"Error fetching lessons for group {group.Name}: {ex.Message}");
                }
            }
        }

        private async Task<int> ProcessLessonsAsync(List<LessonItem> lessonItems,
            Dictionary<string, Group> groupLookup,
            Dictionary<string, Teacher> teacherLookup,
            Dictionary<string, Room> roomLookup,
            int lessonId)
        {
            foreach (var item in lessonItems)
            {
            if (string.IsNullOrEmpty(item.Teacher) || 
                string.IsNullOrEmpty(item.LessonTime) ||
                !DateTime.TryParseExact(item.Date, "dd.MM.yyyy", null, System.Globalization.DateTimeStyles.None, out var lessonDate))
            {
                continue;
            }

            // Parse lesson number
            if (!int.TryParse(item.LessonNumber, out var lessonNumber))
            {
                continue;
            }

            // Find teacher (required)
            if (!teacherLookup.TryGetValue(item.Teacher, out var teacher))
            {
                continue;
            }

            // Find room (optional - can be null for remote lessons)
            Room? room = null;
            if (!string.IsNullOrEmpty(item.Room) && roomLookup.TryGetValue(item.Room, out var foundRoom))
            {
                room = foundRoom;
            }

                // Check if lesson already exists (unique constraint on Date + LessonNumber + TeacherId)
                var existingLesson = await _context.Lessons
                    .FirstOrDefaultAsync(l =>
                        l.Date == lessonDate && l.LessonNumber == lessonNumber && l.TeacherId == teacher.Id);

                if (existingLesson is not null)
                {
                    // Add group to existing lesson if not already present
                    if (groupLookup.TryGetValue(item.Object, out var group))
                    {
                        var existingLessonGroup = await _context.LessonGroups
                            .FirstOrDefaultAsync(lg => lg.LessonId == existingLesson.Id && lg.GroupId == group.Id);

                        if (existingLessonGroup is null)
                        {
                            _context.LessonGroups.Add(new LessonGroup
                            {
                                LessonId = existingLesson.Id,
                                GroupId = group.Id
                            });
                        }
                    }

                    continue;
                }

                // Create new lesson
                var lesson = new Lesson
                {
                    Id = lessonId++,
                    Date = lessonDate,
                    LessonNumber = lessonNumber,
                    LessonName = item.LessonName,
                    LessonTime = item.LessonTime,
                    Comment = item.Comment,
                    Half = item.Half,
                    TeachersAdd = item.TeachersAdd,
                    Group = item.Group,
                    Title = item.Title,
                    Type = item.Type,
                    Replacement = item.Replacement,
                    Reservation = item.Reservation,
                    Online = item.Online,
                    Comment4Link = item.Comment4Link,
                    Link = item.Link,
                    RoomId = room?.Id,
                    TeacherId = teacher.Id
                };

                _context.Lessons.Add(lesson);

                // Add group to lesson
                if (groupLookup.TryGetValue(item.Object, out var groupForLesson))
                {
                    _context.LessonGroups.Add(new LessonGroup
                    {
                        LessonId = lesson.Id,
                        GroupId = groupForLesson.Id
                    });
                }
            }

            return lessonId;
        }
    }
}