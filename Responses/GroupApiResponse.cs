using System.Text.Json.Serialization;

namespace UniversityScheduleApp.Responses;

public sealed class GroupApiResponse
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
            public List<GroupObject> Objects { get; set; } = new();

            public sealed class GroupObject
            {
                [JsonPropertyName("name")]
                public string Name { get; set; } = string.Empty;

                [JsonPropertyName("ID")]
                public string Id { get; set; } = string.Empty;
            }
        }
    }
}