using System.Text.Json.Serialization;

namespace Domain.Entities
{
    public class User
    {
        public Guid Id { get; set; }
        public string Username { get; set; } = default!;
        [JsonIgnore]
        public string Email { get; set; } = default!;
        [JsonIgnore]
        public string Password { get; set; } = default!;
        public DateTime CreationDate { get; set; } = DateTime.UtcNow;
        public bool UserConfirmed { get; set; } = false;
        public bool IsOnline { get; set; } = false;
        public UserProfiles UserProfiles { get; set; } = new UserProfiles();
        public UserActivity UserActivity { get; set; } = new UserActivity();
        public int RoleId { get; set; } = 3;
        public virtual Role Role { get; set; } = default!;
    }
}