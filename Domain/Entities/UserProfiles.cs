using System.Text.Json.Serialization;

namespace Domain.Entities
{
    public class UserProfiles
    {
        [JsonIgnore]
        public int Id { get; set; }
        [JsonIgnore]
        public Guid UserId { get; set; }

        [JsonIgnore]
        public User User { get; set; } = default!;
        public string UserRank { get; set; } = "User";
        public string? AvatarUrl { get; set; }
    }
}