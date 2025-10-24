using System.Text.Json.Serialization;

namespace UniversityScheduleApp.Data.Responses.RoomApiResponse;

public sealed class RoomApiResponse
{
    [JsonPropertyName("psrozklad_export")]
    public PsRozkladExport PsRozkladExport { get; set; } = null!;
}