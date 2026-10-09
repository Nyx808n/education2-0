using System.Collections.Generic;
using TestingPlatform.Data;

namespace TestingPlatform.Models
{
    public class Groups
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public int DirectionId { get; set; }
        public Directions Direction { get; set; } = null!;
        public int CourseId { get; set; }
        public Course Course { get; set; } = null!;
        public int ProjectId { get; set; }
        public Projects Project { get; set; } = null!;
        public List<Student> Students { get; set; } = null!;
        public List<TestingPlatform.Models.Capture> Captures { get; set; } = new();
        public List<Test> Tests { get; set; } = new();
    }
}
