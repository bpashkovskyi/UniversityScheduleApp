using System.Text.Json.Serialization;

namespace UniversityScheduleApp.Data.Responses.LessonApiResponse;

public sealed class LessonItem
{
    [JsonPropertyName("object")]
    public string Object { get; set; } = string.Empty;

    [JsonPropertyName("date")]
    public string Date { get; set; } = string.Empty;

    [JsonPropertyName("comment")]
    public string Comment { get; set; } = string.Empty;

    [JsonPropertyName("lesson_number")]
    public string LessonNumber { get; set; } = string.Empty;

    [JsonPropertyName("lesson_name")]
    public string LessonName { get; set; } = string.Empty;

    [JsonPropertyName("lesson_time")]
    public string LessonTime { get; set; } = string.Empty;

    [JsonPropertyName("half")]
    public string Half { get; set; } = string.Empty;

    [JsonPropertyName("teacher")]
    public string Teacher { get; set; } = string.Empty;

    [JsonPropertyName("teachers_add")]
    public string TeachersAdd { get; set; } = string.Empty;

    [JsonPropertyName("room")]
    public string Room { get; set; } = string.Empty;

    [JsonPropertyName("group")]
    public string Group { get; set; } = string.Empty;

    [JsonPropertyName("title")]
    public string Title { get; set; } = string.Empty;

    [JsonPropertyName("type")]
    public string Type { get; set; } = string.Empty;

    [JsonPropertyName("replacement")]
    public string Replacement { get; set; } = string.Empty;

    [JsonPropertyName("reservation")]
    public string Reservation { get; set; } = string.Empty;

    [JsonPropertyName("online")]
    public string Online { get; set; } = string.Empty;

    [JsonPropertyName("comment4link")]
    public string Comment4Link { get; set; } = string.Empty;

    [JsonPropertyName("link")]
    public string Link { get; set; } = string.Empty;
}