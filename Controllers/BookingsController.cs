using Microsoft.AspNetCore.Mvc;
using MusicBookingAPI.Models.DTO;
using MusicBookingAPI.Services;

namespace MusicBookingAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookingsController : ControllerBase
    {
        private readonly IBookingService _bookingService;

        public BookingsController(IBookingService bookingService)
        {
            _bookingService = bookingService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateBooking(BookingDto bookingDto)
        {
            var booking = await _bookingService.CreateBooking(bookingDto);
            return CreatedAtAction(nameof(CreateBooking), booking);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetBookingById(int id)
        {
            var booking = await _bookingService.GetBookingById(id);
            if (booking == null) return NotFound();
            return Ok(booking);
        }

        [HttpGet("user/{userId}/bookings")]
        public async Task<IActionResult> GetBookingsByUserId(int userId)
        {
            var bookings = await _bookingService.GetBookingsByUserId(userId);
            return Ok(bookings);
        }

        [HttpPut("{id}/cancel")]
        public async Task<IActionResult> CancelBooking(int id)
        {
            var result = await _bookingService.CancelBooking(id);
            if (!result) return NotFound();
            return NoContent();
        }
    }
}
