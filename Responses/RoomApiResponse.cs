using System.Text.Json.Serialization;

namespace UniversityScheduleApp.Responses;

public sealed class RoomApiResponse
{
    [JsonPropertyName("psrozklad_export")]
    public PsRozkladExport PsRozkladExport { get; set; } = null!;

    public sealed class PsRozkladExport
    {
        [JsonPropertyName("blocks")]
        public List<Block> Blocks { get; set; } = new();

        public sealed class Block
        {
            [JsonPropertyName("name")]
            public string Name { get; set; } = string.Empty;

            [JsonPropertyName("objects")]
            public List<RoomObject> Objects { get; set; } = new();

            public sealed class RoomObject
            {
                [JsonPropertyName("name")]
                public string Name { get; set; } = string.Empty;

                [JsonPropertyName("ID")]
                public string Id { get; set; } = string.Empty;
            }
        }
    }
}