using System.Text.Json.Serialization;

namespace UniversityScheduleApp.Responses;

public sealed class TeacherApiResponse
{
    [JsonPropertyName("psrozklad_export")]
    public PsRozkladExport PsRozkladExport { get; set; } = null!;

    public sealed class PsRozkladExport
    {
        [JsonPropertyName("departments")]
        public List<Department> Departments { get; set; } = new();

        public sealed class Department
        {
            [JsonPropertyName("name")]
            public string Name { get; set; } = string.Empty;

            [JsonPropertyName("objects")]
            public List<TeacherObject> Objects { get; set; } = new();

            public sealed class TeacherObject
            {
                [JsonPropertyName("name")]
                public string Name { get; set; } = string.Empty;

                [JsonPropertyName("ID")]
                public string Id { get; set; } = string.Empty;
            }
        }
    }
}