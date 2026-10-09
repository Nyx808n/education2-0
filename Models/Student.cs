using System.Text.RegularExpressions;

namespace TestingPlatform.Models;

public class Student
{
    public int Id { get; set; }
    public string Phone { get; set; } = string.Empty;
    public string VkProfileLink { get; set; } = string.Empty;
    public int UserId { get; set; }
    public User User { get; set; } = null!;
    public TestingPlatform.Models.Groups Group { get; set; } = null!;
    public List<Group> Groups { get; set; } = new();
    public List<Test> Tests { get; set; } = new();
    public List<Attempt> Attempts { get; set; } = new();
    public List<TestResult> TestResults { get; set; } = new();
    public DateTimeOffset CreatedAt { get; set; }
}