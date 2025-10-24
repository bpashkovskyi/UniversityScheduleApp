namespace UniversityScheduleApp.Models;

public sealed class Block
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    
    public ICollection<Room> Rooms { get; set; } = new List<Room>();
}