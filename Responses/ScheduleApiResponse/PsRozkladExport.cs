using System.Text.Json.Serialization;

namespace UniversityScheduleApp.Responses.ScheduleApiResponse;

public sealed class PsRozkladExport
{
    [JsonPropertyName("roz_items")]
    public List<ScheduleItem> ScheduleItems { get; set; } = new();
}