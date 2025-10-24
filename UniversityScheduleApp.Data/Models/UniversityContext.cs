using Microsoft.EntityFrameworkCore;

namespace UniversityScheduleApp.Data.Models
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
        public DbSet<Lesson> Lessons { get; set; }
        public DbSet<LessonGroup> LessonGroups { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Configure Room entity - disable identity for Id column
            modelBuilder.Entity<Room>()
                .Property(r => r.Id)
                .ValueGeneratedNever();

            // Configure Group entity - disable identity for Id column
            modelBuilder.Entity<Group>()
                .Property(g => g.Id)
                .ValueGeneratedNever();

            // Configure Teacher entity - disable identity for Id column
            modelBuilder.Entity<Teacher>()
                .Property(t => t.Id)
                .ValueGeneratedNever();

            // Configure Lesson entity - disable identity for Id column
            modelBuilder.Entity<Lesson>()
                .Property(l => l.Id)
                .ValueGeneratedNever();

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

            // Configure Lesson-Room relationship
            modelBuilder.Entity<Lesson>()
                .HasOne(l => l.Room)
                .WithMany()
                .HasForeignKey(l => l.RoomId)
                .OnDelete(DeleteBehavior.Cascade);

            // Configure Lesson-Teacher relationship
            modelBuilder.Entity<Lesson>()
                .HasOne(l => l.Teacher)
                .WithMany()
                .HasForeignKey(l => l.TeacherId)
                .OnDelete(DeleteBehavior.Cascade);

            // Configure LessonGroup junction table
            modelBuilder.Entity<LessonGroup>()
                .HasKey(lg => new { lg.LessonId, lg.GroupId });

            modelBuilder.Entity<LessonGroup>()
                .HasOne(lg => lg.Lesson)
                .WithMany(l => l.LessonGroups)
                .HasForeignKey(lg => lg.LessonId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<LessonGroup>()
                .HasOne(lg => lg.Group)
                .WithMany()
                .HasForeignKey(lg => lg.GroupId)
                .OnDelete(DeleteBehavior.Cascade);

            // Configure unique constraint for Lesson (Date + LessonNumber + TeacherId)
            // A teacher can't teach multiple lessons at the same time
            modelBuilder.Entity<Lesson>()
                .HasIndex(l => new { l.Date, l.LessonNumber, l.TeacherId })
                .IsUnique();
        }
    }
}