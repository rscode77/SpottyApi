namespace Application.ApplicationUser
{
    public class AddEventDto
    {
        public string Title { get; set; } = default!;
        public string Description { get; set; } = default!;
        public DateTime Date { get; set; }
        public DateTime CreationDate { get; set; }
        public double Lat { get; set; }
        public double Lng { get; set; }
        public string ImageUrl { get; set; } = default!;
    }
}
