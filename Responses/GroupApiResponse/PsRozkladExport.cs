using System.Text.Json.Serialization;

namespace UniversityScheduleApp.Responses.GroupApiResponse;

public sealed class PsRozkladExport
{
    [JsonPropertyName("departments")]
    public List<Department> Departments { get; set; } = new();
}