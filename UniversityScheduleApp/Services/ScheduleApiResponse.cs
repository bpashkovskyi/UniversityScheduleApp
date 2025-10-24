namespace UniversityScheduleApp.UniversityScheduleApp.Services;

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