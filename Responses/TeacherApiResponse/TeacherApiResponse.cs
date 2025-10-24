using System.Text.Json.Serialization;

namespace UniversityScheduleApp.Responses.TeacherApiResponse;

public sealed class TeacherApiResponse
{
    [JsonPropertyName("psrozklad_export")]
    public PsRozkladExport PsRozkladExport { get; set; } = null!;
}