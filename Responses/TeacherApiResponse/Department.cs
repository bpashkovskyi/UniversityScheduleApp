using System.Text.Json.Serialization;

namespace UniversityScheduleApp.Responses.TeacherApiResponse;

public sealed class Department
{
    [JsonPropertyName("name")]
    public string Name { get; set; } = string.Empty;

    [JsonPropertyName("objects")]
    public List<TeacherObject> Objects { get; set; } = new();
}