namespace MusicBookingAPI.Models
{
    public class Artist
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
        public string Name { get; set; }
        public string Genre { get; set; }
        public string Bio { get; set; }
        public string ProfilePicture { get; set; }
    }
}
