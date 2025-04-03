using Microsoft.EntityFrameworkCore;
using MusicBookingAPI.Data;
using MusicBookingAPI.Models;
using MusicBookingAPI.Models.DTO;

namespace MusicBookingAPI.Services
{
    public interface IArtistService
    {
        Task<ArtistDto> CreateArtist(ArtistDto artistDto);
        Task<IEnumerable<ArtistDto>> GetAllArtists();
        Task<ArtistDto> GetArtistById(int id);
        Task<ArtistDto> UpdateArtist(int id, ArtistDto artistDto);
    }
    public class ArtistService : IArtistService
    {
        private readonly ApplicationDbContext _context;

        public ArtistService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<ArtistDto> CreateArtist(ArtistDto artistDto)
        {
            var artist = new Artist
            {
                Name = artistDto.Name,
                Genre = artistDto.Genre,
                Bio = artistDto.Bio
            };
            _context.Artists.Add(artist);
            await _context.SaveChangesAsync();
            return artistDto;
        }

        public async Task<IEnumerable<ArtistDto>> GetAllArtists()
        {
            return await _context.Artists.Select(a => new ArtistDto
            {
                Name = a.Name,
                Genre = a.Genre,
                Bio = a.Bio
            }).ToListAsync();
        }

        public async Task<ArtistDto> GetArtistById(int id)
        {
            var artist = await _context.Artists.FindAsync(id);
            if (artist == null) return null;
            return new ArtistDto { Name = artist.Name, Genre = artist.Genre, Bio = artist.Bio };
        }

        public async Task<ArtistDto> UpdateArtist(int id, ArtistDto artistDto)
        {
            var artist = await _context.Artists.FindAsync(id);
            if (artist == null) return null;

            artist.Name = artistDto.Name;
            artist.Genre = artistDto.Genre;
            artist.Bio = artistDto.Bio;

            await _context.SaveChangesAsync();
            return artistDto;
        }
    }
}
