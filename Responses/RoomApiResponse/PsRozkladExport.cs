using System.Text.Json.Serialization;

namespace UniversityScheduleApp.Responses.RoomApiResponse;

public sealed class PsRozkladExport
{
    [JsonPropertyName("blocks")]
    public List<RoomBlock> Blocks { get; set; } = new();
}