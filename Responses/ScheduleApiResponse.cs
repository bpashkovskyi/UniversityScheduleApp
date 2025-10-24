using System.Text.Json.Serialization;

namespace UniversityScheduleApp.Responses;

public sealed class ScheduleApiResponse
{
    [JsonPropertyName("psrozklad_export")]
    public PsRozkladExport PsRozkladExport { get; set; } = null!;

    public sealed class PsRozkladExport
    {
        [JsonPropertyName("roz_items")]
        public List<ScheduleItem> ScheduleItems { get; set; } = new();

        public sealed class ScheduleItem
        {
            [JsonPropertyName("object")]
            public string Object { get; set; } = string.Empty;

            [JsonPropertyName("date")]
            public string Date { get; set; } = string.Empty;

            [JsonPropertyName("lesson_number")]
            public string LessonNumber { get; set; } = string.Empty;

            [JsonPropertyName("lesson_name")]
            public string LessonName { get; set; } = string.Empty;

            [JsonPropertyName("lesson_time")]
            public string LessonTime { get; set; } = string.Empty;

            [JsonPropertyName("lesson_description")]
            public string LessonDescription { get; set; } = string.Empty;
        }
    }
}