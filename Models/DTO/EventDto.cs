namespace MusicBookingAPI.Models.DTO
{
    public class EventDto
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime Date { get; set; }
        public string Location { get; set; }
        public decimal TicketPrice { get; set; }
        public int AvailableTickets { get; set; }
    }
}
