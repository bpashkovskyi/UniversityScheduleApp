namespace UniversityScheduleApp.Models;

public sealed class Group
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    
    public int GroupDepartmentId { get; set; }
    
    public GroupDepartment GroupDepartment { get; set; } = null!;
};