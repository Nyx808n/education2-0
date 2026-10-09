using static System.Net.Mime.MediaTypeNames;

namespace TestingPlatform.Models
{
    public class TestResult
    {
        public int Id { get; set; }
        public bool Passed { get; set; }
        public int TestId { get; set; }
        public int AttemptId { get; set; }
        public int StudentId { get; set; }

        // Навигации
        public Test Test { get; set; } = null!;
        public Attempt Attempt { get; set; } = null!;
        public Student Student { get; set; } = null!;
    }
}
