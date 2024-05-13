using ApplicationUser.User;
using Domain.Entities;
using System.Text.Json.Serialization;

namespace Application.ApplicationUser
{
    public class EventDto
    {
        public Guid Id { get; set; }
        [JsonIgnore]
        public Guid CreatorId { get; set; }
        public virtual UserDto Creator { get; set; } = default!;
        public string Title { get; set; } = default!;
        public string Description { get; set; } = default!;
        public DateTime Date { get; set; }
        public DateTime CreationDate { get; set; }
        public double Lat { get; set; }
        public double Lng { get; set; }
        public string ImageUrl { get; set; } = default!;
        public virtual List<EventParticipantDto> EventParticipants { get; set; } = default!;
    }
}
