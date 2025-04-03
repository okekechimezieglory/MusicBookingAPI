using Microsoft.EntityFrameworkCore;
using MusicBookingAPI.Data;
using MusicBookingAPI.Models;
using MusicBookingAPI.Models.DTO;

namespace MusicBookingAPI.Services
{
    public interface IBookingService
    {
        Task<BookingDto> CreateBooking(BookingDto bookingDto);
        Task<BookingDto> GetBookingById(int id);
        Task<IEnumerable<BookingDto>> GetBookingsByUserId(int userId);
        Task<bool> CancelBooking(int id);
    }

    public class BookingService : IBookingService
    {
        private readonly ApplicationDbContext _context;

        public BookingService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<BookingDto> CreateBooking(BookingDto bookingDto)
        {
            var booking = new Booking
            {
                EventId = bookingDto.EventId,
                NumberOfTickets = bookingDto.NumberOfTickets,
                TotalPrice = bookingDto.NumberOfTickets * (await _context.Events.Where(e => e.Id == bookingDto.EventId).Select(e => e.TicketPrice).FirstOrDefaultAsync()),
                Status = "pending" // Default status
            };
            _context.Bookings.Add(booking);
            await _context.SaveChangesAsync();
            return bookingDto;
        }

        public async Task<BookingDto> GetBookingById(int id)
        {
            var booking = await _context.Bookings.FindAsync(id);
            if (booking == null) return null;
            return new BookingDto { EventId = booking.EventId, NumberOfTickets = booking.NumberOfTickets };
        }

        public async Task<IEnumerable<BookingDto>> GetBookingsByUserId(int userId)
        {
            return await _context.Bookings
                .Where(b => b.UserId == userId)
                .Select(b => new BookingDto
                {
                    EventId = b.EventId,
                    NumberOfTickets = b.NumberOfTickets
                }).ToListAsync();
        }

        public async Task<bool> CancelBooking(int id)
        {
            var booking = await _context.Bookings.FindAsync(id);
            if (booking == null) return false;

            booking.Status = "canceled";
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
