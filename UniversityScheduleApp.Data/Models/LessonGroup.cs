namespace UniversityScheduleApp.Data.Models;

public sealed class LessonGroup
{
    public int LessonId { get; set; }
    public int GroupId { get; set; }

    // Navigation properties
    public Lesson Lesson { get; set; } = null!;
    public Group Group { get; set; } = null!;
}