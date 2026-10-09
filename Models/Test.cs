using System.Text.RegularExpressions;
using TestingPlatform.Enums;

namespace TestingPlatform.Models
{
    public class Test
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public bool IsRepeatable { get; set; }
        public TestType Type { get; set; }
        public DateTimeOffset CreatedAt { get; set; }
        public DateTimeOffset PublishedAt { get; set; }
        public DateTimeOffset Deadline { get; set; }
        public int? DurationMinutes { get; set; }
        public bool IsPublic { get; set; }
        public int? PassingScore { get; set; }
        public int? MaxAttempts { get; set; }

        // Навигации
        public List<Question> Questions { get; set; } = new();
        public List<Student> Students { get; set; } = new();
        public List<Projects> Projects { get; set; } = new();
        public List<Course> Courses { get; set; } = new();
        public List<Group> Groups { get; set; } = new();
        public List<Directions> Directions { get; set; } = new();
        public List<Attempt> Attempts { get; set; } = new();
        public List<TestResult> TestResults { get; set; } = new();
    }
}
