namespace UniversityScheduleApp.Data.Models;

public sealed class TeacherDepartment
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    
    public ICollection<Teacher> Teachers { get; set; } = new List<Teacher>();
}