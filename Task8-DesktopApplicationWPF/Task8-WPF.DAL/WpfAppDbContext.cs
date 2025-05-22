using Microsoft.EntityFrameworkCore;
using Task8_WPF.DAL.Entities;
using Task8_WPF.DAL.ConnectionSettings;

namespace Task8_WPF.DAL;

public class WpfAppDbContext : DbContext
{
    public DbSet<Course> Courses { get; set; } = null!;
    public DbSet<Teacher> Teachers { get; set; } = null!;
    public DbSet<Group> Groups { get; set; } = null!;
    public DbSet<Student> Students { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Course>(entity =>
        {
            entity.ToTable("COURSES");
            entity.Property(e => e.Id).HasColumnName("COURSE_ID");
            entity.Property(e => e.Name).HasColumnName("NAME");
            entity.Property(e => e.Description).HasColumnName("DESCRIPTION");
        });

        modelBuilder.Entity<Teacher>(entity =>
        {
            entity.ToTable("TEACHERS");
            entity.Property(e => e.Id).HasColumnName("TEACHER_ID");
            entity.Property(e => e.Name).HasColumnName("FIRST_NAME");
            entity.Property(e => e.Surname).HasColumnName("LAST_NAME");
        });

        modelBuilder.Entity<Group>(entity =>
        {
            entity.ToTable("GROUPS");
            entity.Property(e => e.Id).HasColumnName("GROUP_ID");
            entity.Property(e => e.CourseId).HasColumnName("COURSE_ID");
            entity.Property(e => e.TeacherId).HasColumnName("TEACHER_ID");
            entity.Property(e => e.Name).HasColumnName("NAME");
        });

        modelBuilder.Entity<Student>(entity =>
        {
            entity.ToTable("STUDENTS");
            entity.Property(e => e.Id).HasColumnName("STUDENT_ID");
            entity.Property(e => e.GroupId).HasColumnName("GROUP_ID");
            entity.Property(e => e.Name).HasColumnName("FIRST_NAME");
            entity.Property(e => e.Surname).HasColumnName("LAST_NAME");
        });
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            var connectionString = ConfigurationHelper.GetConnectionString();
            optionsBuilder.UseSqlServer(connectionString);
        }
    }
}
