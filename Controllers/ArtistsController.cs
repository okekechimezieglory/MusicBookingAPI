using Microsoft.AspNetCore.Mvc;
using MusicBookingAPI.Models.DTO;
using MusicBookingAPI.Services;

namespace MusicBookingAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ArtistsController : ControllerBase
    {
        private readonly IArtistService _artistService;

        public ArtistsController(IArtistService artistService)
        {
            _artistService = artistService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateArtist(ArtistDto artistDto)
        {
            var artist = await _artistService.CreateArtist(artistDto);
            return CreatedAtAction(nameof(CreateArtist), artist);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllArtists()
        {
            var artists = await _artistService.GetAllArtists();
            return Ok(artists);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetArtistById(int id)
        {
            var artist = await _artistService.GetArtistById(id);
            if (artist == null) return NotFound();
            return Ok(artist);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateArtist(int id, ArtistDto artistDto)
        {
            var updatedArtist = await _artistService.UpdateArtist(id, artistDto);
            if (updatedArtist == null) return NotFound();
            return Ok(updatedArtist);
        }
    }
}
