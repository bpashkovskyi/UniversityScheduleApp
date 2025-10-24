using System.Text.Json.Serialization;

namespace UniversityScheduleApp.Data.Responses.LessonApiResponse;

public sealed class LessonApiResponse
{
    [JsonPropertyName("psrozklad_export")]
    public PsRozkladExport PsRozkladExport { get; set; } = null!;
}