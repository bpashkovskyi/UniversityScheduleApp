using System.Text.Json;
using UniversityScheduleApp.UniversityScheduleApp.Models;

namespace UniversityScheduleApp.UniversityScheduleApp.Services
{
    public class ApiService
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

            foreach (var block in data.psrozklad_export.blocks)
            {
                foreach (var room in block.objects)
                {
                    _context.Rooms.Add(new Room { Id = int.Parse(room.ID), Name = room.name });
                }
            }

            await _context.SaveChangesAsync();
        }

        public async Task FetchAndSaveTeachersAsync()
        {
            var response = await _httpClient.GetStringAsync("https://dekanat.nung.edu.ua/cgi-bin/timetable_export.cgi?req_type=obj_list&req_mode=teacher&show_ID=yes&req_format=json&coding_mode=WINDOWS-1251&bs=ok");
            var data = JsonSerializer.Deserialize<TeacherApiResponse>(response);

            foreach (var department in data.psrozklad_export.departments)
            {
                foreach (var teacher in department.objects)
                {
                    _context.Teachers.Add(new Teacher { Id = int.Parse(teacher.ID), Name = teacher.name });
                }
            }

            await _context.SaveChangesAsync();
        }

        public async Task FetchAndSaveGroupsAsync()
        {
            var response = await _httpClient.GetStringAsync("https://dekanat.nung.edu.ua/cgi-bin/timetable_export.cgi?req_type=obj_list&req_mode=group&show_ID=yes&req_format=json&coding_mode=WINDOWS-1251&bs=ok");
            var data = JsonSerializer.Deserialize<GroupApiResponse>(response);

            foreach (var department in data.psrozklad_export.departments)
            {
                foreach (var group in department.objects)
                {
                    _context.Groups.Add(new Group { Id = int.Parse(group.ID), Name = group.name });
                }
            }

            await _context.SaveChangesAsync();
        }

        public async Task FetchAndSaveSchedulesAsync(int groupId, string startDate, string endDate)
        {
            var url = $"https://dekanat.nung.edu.ua/cgi-bin/timetable_export.cgi?req_type=rozklad&req_mode=group&OBJ_ID={groupId}&begin_date={startDate}&end_date={endDate}&req_format=json&coding_mode=WINDOWS-1251&bs=ok";
            var response = await _httpClient.GetStringAsync(url);
            var data = JsonSerializer.Deserialize<ScheduleApiResponse>(response);

            foreach (var item in data.psrozklad_export.roz_items)
            {
                _context.Schedules.Add(new Schedule
                {
                    Object = item.@object,
                    Date = item.date,
                    LessonNumber = item.lesson_number,
                    LessonName = item.lesson_name,
                    LessonTime = item.lesson_time,
                    LessonDescription = item.lesson_description
                });
            }

            await _context.SaveChangesAsync();
        }
    }

    public class RoomApiResponse
    {
        public PsRozkladExport psrozklad_export { get; set; }

        public class PsRozkladExport
        {
            public List<Block> blocks { get; set; }

            public class Block
            {
                public string name { get; set; }
                public List<RoomObject> objects { get; set; }

                public class RoomObject
                {
                    public string name { get; set; }
                    public string ID { get; set; }
                }
            }
        }
    }

    public class TeacherApiResponse
    {
        public PsRozkladExport psrozklad_export { get; set; }

        public class PsRozkladExport
        {
            public List<Department> departments { get; set; }

            public class Department
            {
                public string name { get; set; }
                public List<TeacherObject> objects { get; set; }

                public class TeacherObject
                {
                    public string name { get; set; }
                    public string ID { get; set; }
                }
            }
        }
    }

    public class GroupApiResponse
    {
        public PsRozkladExport psrozklad_export { get; set; }

        public class PsRozkladExport
        {
            public List<Department> departments { get; set; }

            public class Department
            {
                public string name { get; set; }
                public List<GroupObject> objects { get; set; }

                public class GroupObject
                {
                    public string name { get; set; }
                    public string ID { get; set; }
                }
            }
        }
    }

    public class ScheduleApiResponse
    {
        public PsRozkladExport psrozklad_export { get; set; }

        public class PsRozkladExport
        {
            public List<ScheduleItem> roz_items { get; set; }

            public class ScheduleItem
            {
                public string @object { get; set; }
                public string date { get; set; }
                public string lesson_number { get; set; }
                public string lesson_name { get; set; }
                public string lesson_time { get; set; }
                public string lesson_description { get; set; }
            }
        }
    }
}