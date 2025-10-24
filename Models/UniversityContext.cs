using Microsoft.EntityFrameworkCore;

namespace UniversityScheduleApp.Models
{
    public class UniversityContext : DbContext
    {
        public UniversityContext(DbContextOptions<UniversityContext> options) : base(options)
        {
        }

        public DbSet<Block> Blocks { get; set; }
        public DbSet<Room> Rooms { get; set; }
        public DbSet<GroupDepartment> GroupDepartments { get; set; }
        public DbSet<Group> Groups { get; set; }
        public DbSet<TeacherDepartment> TeacherDepartments { get; set; }
        public DbSet<Teacher> Teachers { get; set; }
        public DbSet<Schedule> Schedules { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Configure Block-Room relationship
            modelBuilder.Entity<Room>()
                .HasOne(r => r.Block)
                .WithMany(b => b.Rooms)
                .HasForeignKey(r => r.BlockId)
                .OnDelete(DeleteBehavior.Cascade);

            // Configure GroupDepartment-Group relationship
            modelBuilder.Entity<Group>()
                .HasOne(g => g.GroupDepartment)
                .WithMany(gd => gd.Groups)
                .HasForeignKey(g => g.GroupDepartmentId)
                .OnDelete(DeleteBehavior.Cascade);

            // Configure TeacherDepartment-Teacher relationship
            modelBuilder.Entity<Teacher>()
                .HasOne(t => t.TeacherDepartment)
                .WithMany(td => td.Teachers)
                .HasForeignKey(t => t.TeacherDepartmentId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}