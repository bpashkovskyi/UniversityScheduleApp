using System.Text.Json;
using Microsoft.EntityFrameworkCore;
using UniversityScheduleApp.Models;
using UniversityScheduleApp.Responses.GroupApiResponse;
using UniversityScheduleApp.Responses.RoomApiResponse;
using UniversityScheduleApp.Responses.ScheduleApiResponse;
using UniversityScheduleApp.Responses.TeacherApiResponse;

namespace UniversityScheduleApp.Services
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
            var response = await _httpClient.GetStringAsync("https://dekanat.nung.edu.ua/cgi-bin/timetable_export.cgi?req_type=obj_list&req_mode=room&show_ID=yes&req_format=json&coding_mode=WINDOWS-1251&bs=ok");
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
            var response = await _httpClient.GetStringAsync("https://dekanat.nung.edu.ua/cgi-bin/timetable_export.cgi?req_type=obj_list&req_mode=teacher&show_ID=yes&req_format=json&coding_mode=WINDOWS-1251&bs=ok");
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
                        TeacherDepartmentId = teacherDepartment.Id
                    };
                    _context.Teachers.Add(teacher);
                }
            }

            await _context.SaveChangesAsync();
        }

        public async Task FetchAndSaveGroupsAsync()
        {
            var response = await _httpClient.GetStringAsync("https://dekanat.nung.edu.ua/cgi-bin/timetable_export.cgi?req_type=obj_list&req_mode=group&show_ID=yes&req_format=json&coding_mode=WINDOWS-1251&bs=ok");
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

        public async Task FetchAndSaveSchedulesAsync(int groupId, string startDate, string endDate)
        {
            var url = $"https://dekanat.nung.edu.ua/cgi-bin/timetable_export.cgi?req_type=rozklad&req_mode=group&OBJ_ID={groupId}&begin_date={startDate}&end_date={endDate}&req_format=json&coding_mode=WINDOWS-1251&bs=ok";
            var response = await _httpClient.GetStringAsync(url);
            var data = JsonSerializer.Deserialize<ScheduleApiResponse>(response);

            foreach (var item in data.PsRozkladExport.ScheduleItems)
            {
                _context.Schedules.Add(new Schedule
                {
                    Object = item.Object,
                    Date = item.Date,
                    LessonNumber = item.LessonNumber,
                    LessonName = item.LessonName,
                    LessonTime = item.LessonTime,
                    LessonDescription = item.LessonDescription
                });
            }

            await _context.SaveChangesAsync();
        }
    }
}