using ApplicationUser.User;

namespace Application.ApplicationUser
{
    public class UserActivityDto
    {
        public UserDto? User { get; set; }
        public double Lat { get; set; }
        public double Lon { get; set; }
        public DateTime LastActivity { get; set; }
    }
}
