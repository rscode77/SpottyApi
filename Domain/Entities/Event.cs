namespace Domain.Entities
{
    public class Event
    {
        public Guid Id { get; set; }
        public Guid CreatorId { get; set; }
        public virtual User Creator { get; set; } = default!;
        public string Title { get; set; } = default!;
        public string Description { get; set; } = default!;
        public DateTime Date { get; set; }
        public DateTime CreationDate { get; set; }
        public double Lat { get; set; }
        public double Lng { get; set; }
        public string ImageUrl { get; set; } = default!;
        public virtual List<EventParticipant> EventParticipant { get; set; } = default!;
    }
}
