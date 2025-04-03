using Microsoft.EntityFrameworkCore;
using MusicBookingAPI.Data;
using MusicBookingAPI.Models;
using MusicBookingAPI.Models.DTO;

namespace MusicBookingAPI.Services
{
    public interface IUserService
    {
        Task<UserDto> RegisterUser(UserDto userDto);
        Task<string> LoginUser(string username, string password);
        Task<UserDto> GetUserById(int id);
    }
    public class UserService : IUserService
    {
        private readonly ApplicationDbContext _context;

        public UserService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<UserDto> RegisterUser(UserDto userDto)
        {
            var user = new User
            {
                Username = userDto.Username,
                Email = userDto.Email,
                Password = BCrypt.Net.BCrypt.HashPassword(userDto.Password),
                Role = "customer"
            };
            _context.Users.Add(user);
            await _context.SaveChangesAsync();
            return userDto;
        }

        public async Task<string> LoginUser(string username, string password)
        {
            var user = await _context.Users.SingleOrDefaultAsync(u => u.Username == username);
            if (user == null || !BCrypt.Net.BCrypt.Verify(password, user.Password))
            {
                throw new UnauthorizedAccessException("Invalid credentials");
            }
           
            return "generated_token";
        }

        public async Task<UserDto> GetUserById(int id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null) return null;
            return new UserDto { Username = user.Username, Email = user.Email };
        }
    }
}
