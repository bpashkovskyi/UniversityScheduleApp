using System.Text.Json.Serialization;

namespace UniversityScheduleApp.Data.Responses.RoomApiResponse;

public sealed class RoomObject
{
    [JsonPropertyName("name")]
    public string Name { get; set; } = string.Empty;

    [JsonPropertyName("ID")]
    public string Id { get; set; } = string.Empty;
}