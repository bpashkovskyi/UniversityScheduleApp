namespace UniversityScheduleApp.Data.Models;

public sealed class Lesson
{
    public int Id { get; set; }
    public DateTime Date { get; set; }
    public int LessonNumber { get; set; }
    public string LessonName { get; set; } = string.Empty;
    public string LessonTime { get; set; } = string.Empty;
    public string Comment { get; set; } = string.Empty;
    public string Half { get; set; } = string.Empty;
    public string TeachersAdd { get; set; } = string.Empty;
    public string Group { get; set; } = string.Empty;
    public string Title { get; set; } = string.Empty;
    public string Type { get; set; } = string.Empty;
    public string Replacement { get; set; } = string.Empty;
    public string Reservation { get; set; } = string.Empty;
    public string Online { get; set; } = string.Empty;
    public string Comment4Link { get; set; } = string.Empty;
    public string Link { get; set; } = string.Empty;

    // Foreign keys
    public int? RoomId { get; set; }
    public int TeacherId { get; set; }

    // Navigation properties
    public Room? Room { get; set; }
    public Teacher Teacher { get; set; } = null!;
    public ICollection<LessonGroup> LessonGroups { get; set; } = new List<LessonGroup>();
}