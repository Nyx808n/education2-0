using System.Text.RegularExpressions;
using TestingPlatform.Models;

namespace TestingPlatform.Models
{
    public class Directions
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public List<Groups> Group { get; set; } = new();
        public List<Test> Tests { get; set; } = new();
    }
}