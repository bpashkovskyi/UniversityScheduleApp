namespace UniversityScheduleApp.UniversityScheduleApp.Services;

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