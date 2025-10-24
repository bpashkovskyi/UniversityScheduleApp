using Microsoft.EntityFrameworkCore;

namespace UniversityScheduleApp.UniversityScheduleApp.Models
{
    public class UniversityContext : DbContext
    {
        public UniversityContext(DbContextOptions<UniversityContext> options) : base(options) { }

        public DbSet<Room> Rooms { get; set; }
        public DbSet<Teacher> Teachers { get; set; }
        public DbSet<Group> Groups { get; set; }
        public DbSet<Schedule> Schedules { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Room>().ToTable("Rooms");
            modelBuilder.Entity<Teacher>().ToTable("Teachers");
            modelBuilder.Entity<Group>().ToTable("Groups");
            modelBuilder.Entity<Schedule>().ToTable("Schedules");
        }
    }

    public class Room
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }

    public class Teacher
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }

    public class Group
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }

    public class Schedule
    {
        public int Id { get; set; }
        public string Object { get; set; }
        public string Date { get; set; }
        public string LessonNumber { get; set; }
        public string LessonName { get; set; }
        public string LessonTime { get; set; }
        public string LessonDescription { get; set; }
    }
}