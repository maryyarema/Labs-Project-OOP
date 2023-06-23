using Microsoft.EntityFrameworkCore;
using P01_StudentSystem.Data.Models;
using P01_StudentSystem.Data.Configuration;
namespace P01_StudentSystem.Data
{
    public class StudentSystemContext : DbContext
    {
        public DbSet<Student> Students { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<Resource> Resources { get; set; }
        public DbSet<Homework> Homeworks { get; set; }
        public DbSet<StudentCourse> StudentsCourses { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Data Source=DESKTOP-LBAOKKU\\SQLEXPRESS;Initial Catalog=EFStudentSystemDB;Integrated Security=True;Connect Timeout=30;Encrypt=False;");
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<StudentCourse>()
                 .HasKey(t => new { t.StudentId, t.CourseId });

            modelBuilder.Entity<Student>()
                .HasMany(s => s.StudentsCources)
                .WithOne(s => s.Student)
                .HasForeignKey(s => s.StudentId)
                .HasPrincipalKey(s => s.StudentId);

            modelBuilder.Entity<Student>()
                .HasMany(s => s.Homeworks)
                .WithOne(s => s.Student)
                .HasForeignKey(s => s.StudentId)
                .HasPrincipalKey(s => s.StudentId);

            modelBuilder.Entity<Student>()
                .Property(s => s.Name)
                .HasMaxLength(100)
                .IsUnicode(true);

            modelBuilder.Entity<Student>()
                .Property(s => s.PhoneNumber)
                .HasMaxLength(10)
                .IsFixedLength(true)
                .IsUnicode(false);

            modelBuilder.Entity<Student>()
                .Property(s => s.Birthdaty)
                .IsRequired(false);

            modelBuilder.Entity<Course>()
                .HasMany(s => s.StudentsCources)
                .WithOne(s => s.Course)
                .HasForeignKey(s => s.CourseId)
                .HasPrincipalKey(s => s.CourseId);

            modelBuilder.Entity<Course>()
                .HasMany(s => s.Homeworks)
                .WithOne(s => s.Course)
                .HasForeignKey(s => s.CourseId)
                .HasPrincipalKey(s => s.CourseId);

            modelBuilder.Entity<Course>()
                .HasMany(s => s.Resources)
                .WithOne(s => s.Course)
                .HasForeignKey(s => s.CourseId)
                .HasPrincipalKey(s => s.CourseId);

            modelBuilder.Entity<Course>()
                .Property(c => c.Name)
                .HasMaxLength(80)
                .IsUnicode(true);

            modelBuilder.Entity<Course>()
                .Property(c => c.Description)
            .IsUnicode(true)
                .IsRequired(false);

            modelBuilder.Entity<Resource>()
                .Property(c => c.Name)
            .HasMaxLength(50)
                .IsUnicode(true);

            modelBuilder.Entity<Resource>()
                .Property(c => c.Url)
                .IsUnicode(false);

            modelBuilder.Entity<Homework>()
                .Property(h => h.Content)
                .HasColumnType("nvarchar(max)")
                .HasColumnName("Content")
                .IsUnicode(false);

            modelBuilder.ApplyConfiguration(new CourseConfiguration());
            modelBuilder.ApplyConfiguration(new ResourceConfiguration());
            modelBuilder.ApplyConfiguration(new StudentConfiguration());
            modelBuilder.ApplyConfiguration(new StudentCourseConfiguration());
            modelBuilder.ApplyConfiguration(new HomeworkConfiguration());
        }
    }
}
