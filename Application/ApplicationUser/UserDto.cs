using Domain.Entities;

namespace ApplicationUser.User
{
    public class UserDto
    {
        public Guid Id { get; set; }
        public string Username { get; set; } = default!;
        public DateTime CreationDate { get; set; }
        public bool IsOnline { get; set; }
        public UserProfiles UserProfiles { get; set; } = default!;
    }
}