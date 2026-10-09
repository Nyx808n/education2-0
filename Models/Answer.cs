namespace TestingPlatform.Models
{
    public class Answer
    {
        public int Id { get; set; }
        public string Text { get; set; } = string.Empty;
        public bool IsCorrect { get; set; }
        public int QuestionId { get; set; }

        // Навигации
        public Question Question { get; set; } = null!;
        public List<UserSelectedOption> UserSelectedOptions { get; set; } = new();
    }
}
