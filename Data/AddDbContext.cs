using Microsoft.EntityFrameworkCore;
using System.Text.RegularExpressions;
using TestingPlatform.Enums;
using TestingPlatform.Models;
using MyGroupModel = TestingPlatform.Models.Groups;

namespace TestingPlatform.Data;

public class CaptureCollection : DbContext
{
    public CaptureCollection(DbContextOptions<CaptureCollection> options) : base(options)
    {
    }
    public DbSet<User> Users => Set<User>();
    public DbSet<Student> Students => Set<Student>();
    public DbSet<Projects> Projects => Set<Projects>();
    public DbSet<TestingPlatform.Models.Groups> Groups => Set<TestingPlatform.Models.Groups>();
    public DbSet<Directions> Directions => Set<Directions>();
    public DbSet<Course> Courses => Set<Course>();
    public DbSet<Test> Tests => Set<Test>();
    public DbSet<Question> Questions => Set<Question>();
    public DbSet<Answer> Answers => Set<Answer>();
    public DbSet<Attempt> Attempts => Set<Attempt>();
    public DbSet<UserAttemptAnswer> UserAttemptAnswers => Set<UserAttemptAnswer>();
    public DbSet<UserSelectedOption> UserSelectedOptions => Set<UserSelectedOption>();
    public DbSet<UserTextAnswer> UserTextAnswers => Set<UserTextAnswer>();
    public DbSet<TestResult> TestResults => Set<TestResult>();
    public DbSet<Models.Capture> Captures => Set<Models.Capture>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Ignore<System.Text.RegularExpressions.Capture>();
        modelBuilder.Ignore<System.Text.RegularExpressions.Group>();

        modelBuilder.Entity<User>(e =>
        {
            e.HasKey(x => x.Id);
            e.HasIndex(x => x.Login).IsUnique();
            e.Property(x => x.PasswordHash).IsRequired();
            e.HasIndex(x => x.Email).IsUnique();
            e.Property(x => x.Login).IsRequired();
            e.Property(x => x.Email).IsRequired();
            e.Property(x => x.CreatedAt).HasDefaultValueSql("CURRENT_TIMESTAMP");
            e.Property(x => x.UserRole).HasConversion<string>().IsRequired();

            e.HasOne(x => x.Student)
                .WithOne(s => s.User)
                .HasForeignKey<Student>(s => s.UserId)
                .OnDelete(DeleteBehavior.Cascade);
        });

        modelBuilder.Entity<Student>(e =>
        {
            e.HasKey(x => x.Id);

            e.Property(x => x.Phone)
                .IsRequired()
                .HasMaxLength(30);

            e.Property(x => x.VkProfileLink)
                .IsRequired();
            e.HasIndex(x => x.UserId).IsUnique();
        });

        // Валидация моделей Directions, Course, Projects

        modelBuilder.Entity<Directions>(e =>
        {
            e.HasKey(x => x.Id);
            e.Property(x => x.Name).IsRequired();
            e.HasIndex(x => x.Name).IsUnique();
        });

        modelBuilder.Entity<Course>(e =>
        {
            e.HasKey(x => x.Id);
            e.Property(x => x.Name).IsRequired();
            e.HasIndex(x => x.Name).IsUnique();
        });

        modelBuilder.Entity<Projects>(e =>
        {
            e.HasKey(x => x.Id);
            e.Property(x => x.Name).IsRequired();
            e.HasIndex(x => x.Name).IsUnique();
        });

        // Валидация модели Group
        modelBuilder.Entity<Groups>(e =>
        {
            e.HasKey(x => x.Id);
            e.Property(x => x.Name).IsRequired();
            e.HasIndex(x => x.Name).IsUnique();

            e.HasOne(x => x.Direction)
                .WithMany(d => d.Group)
                .HasForeignKey(x => x.DirectionId)
                .OnDelete(DeleteBehavior.Restrict);

            e.HasOne(x => x.Course)
                .WithMany(c => c.Group)
                .HasForeignKey(x => x.CourseId)
                .OnDelete(DeleteBehavior.Restrict);

            e.HasOne(x => x.Project)
                .WithMany(p => p.Group)
                .HasForeignKey(x => x.ProjectId)
                .OnDelete(DeleteBehavior.Restrict);
        });

        modelBuilder.Entity<Test>(e =>
        {
            e.HasKey(x => x.Id);
            e.Property(x => x.Title).IsRequired();
            e.Property(x => x.Description).IsRequired();
            e.Property(x => x.IsRepeatable).HasDefaultValue(false);
            e.Property(x => x.Type).HasConversion<string>().IsRequired();
            e.Property(x => x.CreatedAt).HasDefaultValueSql("CURRENT_TIMESTAMP");
            e.Property(x => x.IsPublic).HasDefaultValue(false);

            e.HasMany(x => x.Students).WithMany(x => x.Tests).UsingEntity(j => j.ToTable("test_students"));
            e.HasMany(x => x.Groups).WithMany("Tests").UsingEntity(j => j.ToTable("test_groups"));

            e.HasMany(x => x.Projects).WithMany(x => x.Tests).UsingEntity(j => j.ToTable("test_projects"));
            e.HasMany(x => x.Courses).WithMany(x => x.Tests).UsingEntity(j => j.ToTable("test_courses"));
            e.HasMany(x => x.Directions).WithMany(x => x.Tests).UsingEntity(j => j.ToTable("test_directions"));
        });

        modelBuilder.Entity<Question>(e =>
        {
            e.HasKey(x => x.Id);
            e.Property(x => x.Text).IsRequired();
            e.Property(x => x.Description).HasMaxLength(2000);
            e.Property(x => x.AnswerType).HasConversion<string>().IsRequired();
            e.Property(x => x.IsScoring).HasDefaultValue(true);

            e.HasIndex(x => new { x.TestId, x.Number }).IsUnique();

            e.HasOne(x => x.Test).WithMany(t => t.Questions).HasForeignKey(x => x.TestId).OnDelete(DeleteBehavior.Cascade);
        });

        modelBuilder.Entity<Answer>(e =>
        {
            e.HasKey(x => x.Id);
            e.Property(x => x.Text).IsRequired();
            e.Property(x => x.IsCorrect).IsRequired();

            e.HasOne(x => x.Question).WithMany(q => q.Answers).HasForeignKey(x => x.QuestionId).OnDelete(DeleteBehavior.Cascade);
        });

        modelBuilder.Entity<Attempt>(e =>
        {
            e.HasKey(x => x.Id);
            e.Property(x => x.StartedAt).HasDefaultValueSql("CURRENT_TIMESTAMP");

            e.HasOne(x => x.Test).WithMany(t => t.Attempts).HasForeignKey(x => x.TestId).OnDelete(DeleteBehavior.Restrict);
            e.HasOne(x => x.Student).WithMany(s => s.Attempts).HasForeignKey(x => x.StudentId).OnDelete(DeleteBehavior.Cascade);
        });

        modelBuilder.Entity<UserAttemptAnswer>(e =>
        {
            e.HasKey(x => x.Id);
            e.HasIndex(x => new { x.AttemptId, x.QuestionId }).IsUnique();

            e.HasOne(x => x.Attempt).WithMany(a => a.UserAttemptAnswers).HasForeignKey(x => x.AttemptId).OnDelete(DeleteBehavior.Cascade);
            e.HasOne(x => x.Question).WithMany(q => q.UserAttemptAnswers).HasForeignKey(x => x.QuestionId).OnDelete(DeleteBehavior.Restrict);
        });

        modelBuilder.Entity<UserSelectedOption>(e =>
        {
            e.HasKey(x => x.Id);
            e.HasOne(x => x.UserAttemptAnswer).WithMany(u => u.UserSelectedOptions).HasForeignKey(x => x.UserAttemptAnswerId).OnDelete(DeleteBehavior.Cascade);
            e.HasOne(x => x.Answer).WithMany(a => a.UserSelectedOptions).HasForeignKey(x => x.AnswerId).OnDelete(DeleteBehavior.Restrict);
        });

        modelBuilder.Entity<UserTextAnswer>(e =>
        {
            e.HasKey(x => x.Id);
            e.Property(x => x.TextAnswer).IsRequired();
            e.HasIndex(x => x.UserAttemptAnswerId).IsUnique();

            e.HasOne(x => x.UserAttemptAnswer)
                .WithOne(u => u.UserTextAnswer)
                .HasForeignKey<UserTextAnswer>(x => x.UserAttemptAnswerId)
                .OnDelete(DeleteBehavior.Cascade);
        });

        modelBuilder.Entity<TestResult>(e =>
        {
            e.HasKey(x => x.Id);
            e.HasIndex(x => new { x.TestId, x.StudentId, x.AttemptId }).IsUnique();

            e.HasOne(x => x.Test).WithMany(t => t.TestResults).HasForeignKey(x => x.TestId).OnDelete(DeleteBehavior.Restrict);
            e.HasOne(x => x.Attempt).WithMany(a => a.TestResults).HasForeignKey(x => x.AttemptId).OnDelete(DeleteBehavior.Cascade);
            e.HasOne(x => x.Student).WithMany(s => s.TestResults).HasForeignKey(x => x.StudentId).OnDelete(DeleteBehavior.Cascade);
        });

        base.OnModelCreating(modelBuilder);

    }
}
