namespace Domain.Entities
{
	public class EventLogs
	{
		public Guid Id { get; set; }
		public DateTime Date { get; set; } = DateTime.Now;
		public string Exception { get; set; } = default!;
		public string Message { get; set; } = default!;
	}
}