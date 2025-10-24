using System.Text.Json.Serialization;

namespace UniversityScheduleApp.Responses.GroupApiResponse;

public sealed class Department
{
    [JsonPropertyName("name")]
    public string Name { get; set; } = string.Empty;

    [JsonPropertyName("objects")]
    public List<GroupObject> Objects { get; set; } = new();
}