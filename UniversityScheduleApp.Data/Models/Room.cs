namespace UniversityScheduleApp.Data.Models;

public sealed class Room
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;

    public int BlockId { get; set; }
    public Block Block { get; set; } = null!;
}