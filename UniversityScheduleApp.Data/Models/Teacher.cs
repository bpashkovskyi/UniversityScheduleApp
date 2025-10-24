namespace UniversityScheduleApp.Data.Models;

public sealed class Teacher
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Surname { get; set; } = string.Empty;
    public string FirstName { get; set; } = string.Empty;
    public string Patronymic { get; set; } = string.Empty;
    
    // Foreign key
    public int TeacherDepartmentId { get; set; }
    
    // Navigation property
    public TeacherDepartment TeacherDepartment { get; set; } = null!;
}