using Microsoft.EntityFrameworkCore;
using MusicBookingAPI.Data;
using MusicBookingAPI.Models;
using MusicBookingAPI.Models.DTO;

namespace MusicBookingAPI.Services
{
    public interface IEventService
    {
        Task<EventDto> CreateEvent(EventDto eventDto);
        Task<IEnumerable<EventDto>> GetAllEvents();
        Task<EventDto> GetEventById(int id);
        Task<EventDto> UpdateEvent(int id, EventDto eventDto);
    }

    public class EventService : IEventService
    {
        private readonly ApplicationDbContext _context;

        public EventService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<EventDto> CreateEvent(EventDto eventDto)
        {
            var eventEntity = new Event
            {
                Title = eventDto.Title,
                Description = eventDto.Description,
                Date = eventDto.Date,
                Location = eventDto.Location,
                TicketPrice = eventDto.TicketPrice,
                AvailableTickets = eventDto.AvailableTickets
            };
            _context.Events.Add(eventEntity);
            await _context.SaveChangesAsync();
            return eventDto;
        }

        public async Task<IEnumerable<EventDto>> GetAllEvents()
        {
            return await _context.Events.Select(e => new EventDto
            {
                Title = e.Title,
                Description = e.Description,
                Date = e.Date,
                Location = e.Location,
                TicketPrice = e.TicketPrice,
                AvailableTickets = e.AvailableTickets
            }).ToListAsync();
        }

        public async Task<EventDto> GetEventById(int id)
        {
            var eventEntity = await _context.Events.FindAsync(id);
            if (eventEntity == null) return null;
            return new EventDto { Title = eventEntity.Title, Description = eventEntity.Description, Date = eventEntity.Date, Location = eventEntity.Location, TicketPrice = eventEntity.TicketPrice, AvailableTickets = eventEntity.AvailableTickets };
        }

        public async Task<EventDto> UpdateEvent(int id, EventDto eventDto)
        {
            var eventEntity = await _context.Events.FindAsync(id);
            if (eventEntity == null) return null;

            eventEntity.Title = eventDto.Title;
            eventEntity.Description = eventDto.Description;
            eventEntity.Date = eventDto.Date;
            eventEntity.Location = eventDto.Location;
            eventEntity.TicketPrice = eventDto.TicketPrice;
            eventEntity.AvailableTickets = eventDto.AvailableTickets;

            await _context.SaveChangesAsync();
            return eventDto;
        }
    }
}
