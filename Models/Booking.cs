namespace MusicBookingAPI.Models
{
    public class Booking
    {
        public int Id { get; set; }
        public int EventId { get; set; }
        public Event Event { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
        public int NumberOfTickets { get; set; }
        public decimal TotalPrice { get; set; }
        public string Status { get; set; }
    }
}
