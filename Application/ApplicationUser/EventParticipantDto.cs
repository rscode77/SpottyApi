using ApplicationUser.User;
using System.Text.Json.Serialization;

namespace Application.ApplicationUser
{
    public class EventParticipantDto
    {
        [JsonIgnore]
        public Guid Id { get; set; }
        [JsonIgnore]
        public Guid EventId { get; set; }

        public UserDto User { get; set; } = default!;

        public DateTime RegistrationDate { get; set; }
    }
}
