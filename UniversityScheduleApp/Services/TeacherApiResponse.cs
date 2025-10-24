namespace UniversityScheduleApp.UniversityScheduleApp.Services;

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