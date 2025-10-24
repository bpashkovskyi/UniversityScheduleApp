namespace UniversityScheduleApp.Data.Models;

public sealed class GroupDepartment
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    
    public ICollection<Group> Groups { get; set; } = new List<Group>();
}