using System.Text.Json.Serialization;

namespace UniversityScheduleApp.Data.Responses.TeacherApiResponse;

public sealed class TeacherObject
{
    [JsonPropertyName("name")]
    public string Name { get; set; } = string.Empty;

    [JsonPropertyName("ID")]
    public string Id { get; set; } = string.Empty;
}