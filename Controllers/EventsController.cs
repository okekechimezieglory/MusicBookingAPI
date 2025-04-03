using Microsoft.AspNetCore.Mvc;
using MusicBookingAPI.Models.DTO;
using MusicBookingAPI.Services;

namespace MusicBookingAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EventsController : ControllerBase
    {
        private readonly IEventService _eventService;

        public EventsController(IEventService eventService)
        {
            _eventService = eventService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateEvent(EventDto eventDto)
        {
            var eventCreated = await _eventService.CreateEvent(eventDto);
            return CreatedAtAction(nameof(CreateEvent), eventCreated);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllEvents()
        {
            var events = await _eventService.GetAllEvents();
            return Ok(events);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetEventById(int id)
        {
            var eventEntity = await _eventService.GetEventById(id);
            if (eventEntity == null) return NotFound();
            return Ok(eventEntity);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateEvent(int id, EventDto eventDto)
        {
            var updatedEvent = await _eventService.UpdateEvent(id, eventDto);
            if (updatedEvent == null) return NotFound();
            return Ok(updatedEvent);
        }
    }

}
