namespace Domain.Entities
{
    public class UserActivity
    {
        public int Id { get; set; }
        public Guid UserId { get; set; }
        public User? User { get; set; }
        public double Lat { get; set; }
        public double Lon { get; set; }
        public DateTime LastActivity { get; set; } = DateTime.UtcNow;
        public bool IsBanned { get; set; } = false;
        public string? BanReason { get; set; }
    }
}