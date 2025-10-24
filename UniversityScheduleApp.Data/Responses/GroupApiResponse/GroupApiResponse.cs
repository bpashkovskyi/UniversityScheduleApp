using System.Text.Json.Serialization;

namespace UniversityScheduleApp.Data.Responses.GroupApiResponse;

public sealed class GroupApiResponse
{
    [JsonPropertyName("psrozklad_export")]
    public PsRozkladExport PsRozkladExport { get; set; } = null!;
}