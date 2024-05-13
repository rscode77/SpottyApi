namespace Domain.Entities
{
    public class EventParticipant
    {
        public Guid Id { get; set; } = default!;

        public Guid EventId { get; set; } = default!;
        public virtual Event Event { get; set; } = default!;

        public Guid UserId { get; set; } = default!;
        public virtual User User { get; set; } = default!;

        public DateTime RegistrationDate { get; set; } = DateTime.Now;
    }
}