using Microsoft.EntityFrameworkCore;

namespace UniversityScheduleApp.Models
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
}