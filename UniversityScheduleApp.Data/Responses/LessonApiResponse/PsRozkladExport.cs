using System.Text.Json.Serialization;

namespace UniversityScheduleApp.Data.Responses.LessonApiResponse;

public sealed class PsRozkladExport
{
    [JsonPropertyName("roz_items")]
    public List<LessonItem> RozItems { get; set; } = new();
}