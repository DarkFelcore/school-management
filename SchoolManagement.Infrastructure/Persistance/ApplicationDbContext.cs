using Microsoft.EntityFrameworkCore;
using SchoolManagement.Domain.Common.JoinedEntities;
using SchoolManagement.Domain.Lessons;
using SchoolManagement.Domain.Lessons.Enums;
using SchoolManagement.Domain.Lessons.ValueObjects;
using SchoolManagement.Domain.Schools;
using SchoolManagement.Domain.Schools.Enums;
using SchoolManagement.Domain.Schools.ValueObjects;
using SchoolManagement.Domain.Students;
using SchoolManagement.Domain.Students.ValueObjects;
using SchoolManagement.Domain.Users;
using SchoolManagement.Domain.Users.ValueObjects;

namespace SchoolManagement.Infrastructure.Persistance
{
    // -s: -startup-project
    // -p: -project
    // RUN MIGRATION: dotnet ef migrations add InitialCreate -p .\BuberDinner.Infrastructure\ -s .\BuberDinner.Api\
    // UPDATE DATABASE: dotnet ef database update -p .\BuberDinner.Infrastructure\ -s .\BuberDinner.Api\
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<Student> Students { get; set; }
        public DbSet<Lesson> Lessons { get; set; }
        public DbSet<School> Schools { get; set; }
        public DbSet<StudentLessons> StudentLessons { get; set; }
        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Student
            modelBuilder.Entity<Student>()
                .HasKey(l => l.StudentId);
            modelBuilder.Entity<Student>()
                .Property(l => l.StudentId)
                .HasConversion
                (
                    id => id.Value,
                    value => new StudentId(value)
                );

            // Lesson
            modelBuilder.Entity<Lesson>()
                .HasKey(l => l.LessonId);
            modelBuilder.Entity<Lesson>()
                .Property(l => l.LessonId)
                .HasConversion
                (
                    id => id.Value,
                    value => new LessonId(value)
                );
            modelBuilder.Entity<Lesson>()
                .Property(l => l.Subject)
                .HasConversion(
                    v => v.ToString(),  // Way in
                    v => (LessonSubject)Enum.Parse(typeof(LessonSubject), v) // Way out
                );

            // School
            modelBuilder.Entity<School>()
                .HasKey(l => l.SchoolId);
            modelBuilder.Entity<School>()
                .Property(l => l.SchoolId)
                .HasConversion
                (
                    id => id.Value,
                    value => new SchoolId(value)
                );
            modelBuilder.Entity<School>()
                .Property(sc => sc.SchoolType)
                .HasConversion(
                    v => v.ToString(),  // Way in
                    v => (SchoolType)Enum.Parse(typeof(SchoolType), v) // Way out
                );

            // Relationschips //

            // School/Student - Many to one realtionship
            modelBuilder.Entity<School>()
                .HasMany(sc => sc.Students)
                .WithOne(s => s.School)
                .HasForeignKey(s => s.SchoolId);

            // Joined entity - Student/Lesson - Many to many relationship
            modelBuilder.Entity<StudentLessons>().HasKey(sc => new { sc.StudentId, sc.LessonId });
            modelBuilder.Entity<StudentLessons>()
                .HasOne(sc => sc.Student)
                .WithMany(s => s.StudentLessons)
                .HasForeignKey(sc => sc.StudentId);
            modelBuilder.Entity<StudentLessons>()
                .HasOne(sc => sc.Lesson)
                .WithMany(s => s.StudentLessons)
                .HasForeignKey(sc => sc.LessonId);

            // Users
            modelBuilder.Entity<User>()
                .HasKey(u => u.UserId);

            modelBuilder.Entity<User>()
                .Property(l => l.UserId)
                .HasConversion
                (
                    id => id.Value,
                    value => new UserId(value)
                );
        }

    }
}
