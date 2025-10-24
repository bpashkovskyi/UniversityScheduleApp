using System.Text.Json.Serialization;

namespace UniversityScheduleApp.Data.Responses.RoomApiResponse;

public sealed class RoomBlock
{
    [JsonPropertyName("name")]
    public string Name { get; set; } = string.Empty;

    [JsonPropertyName("objects")]
    public List<RoomObject> Objects { get; set; } = new();
}