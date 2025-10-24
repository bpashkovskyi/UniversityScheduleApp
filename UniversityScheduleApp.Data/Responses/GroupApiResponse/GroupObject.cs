using System.Text.Json.Serialization;

namespace UniversityScheduleApp.Data.Responses.GroupApiResponse;

public sealed class GroupObject
{
    [JsonPropertyName("name")]
    public string Name { get; set; } = string.Empty;

    [JsonPropertyName("ID")]
    public string Id { get; set; } = string.Empty;
}