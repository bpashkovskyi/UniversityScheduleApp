using System.Text.Json.Serialization;

namespace UniversityScheduleApp.Responses.ScheduleApiResponse;

public sealed class ScheduleApiResponse
{
    [JsonPropertyName("psrozklad_export")]
    public PsRozkladExport PsRozkladExport { get; set; } = null!;
}