namespace UniversityScheduleApp.Models;

public sealed class Teacher
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    
    // Foreign key
    public int TeacherDepartmentId { get; set; }
    
    // Navigation property
    public TeacherDepartment TeacherDepartment { get; set; } = null!;
};