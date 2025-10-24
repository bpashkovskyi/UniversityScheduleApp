namespace UniversityScheduleApp.UniversityScheduleApp.Services;

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