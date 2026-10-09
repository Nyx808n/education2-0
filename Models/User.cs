using TestingPlatform.Enums;
using System.Text.Json.Serialization;

namespace TestingPlatform.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Login { get; set; } = string.Empty;
        public string PasswordHash { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string FirstName { get; set; } = string.Empty;
        public string? MiddleName { get; set; }
        public string LastName { get; set; } = string.Empty;
        public string Phone { get; set; } = null!;
        public string VkProfileLink { get; set; } = null!;
        public required string UserRole { get; set; } = null!;
        public DateTimeOffset CreatedAt { get; set; } = DateTimeOffset.UtcNow;
        [JsonIgnore]
        /// <summary>
        /// Если роль студент - должна быть запись в таблице Student
        /// </summary>
        public Student? Student { get; set; }

    }

}

