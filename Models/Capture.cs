namespace TestingPlatform.Models
{
    public class Capture
    {
        public int Id { get; set; }
        public string Text { get; set; } = string.Empty;
        public int Index { get; set; }
        public int Length { get; set; }

        public Capture() { }

    }
}
