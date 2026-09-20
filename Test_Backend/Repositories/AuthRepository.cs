using Microsoft.EntityFrameworkCore;
using Test_Backend.Data;
using Test_Backend.Model;

namespace Test_Backend.Repositories
{
    public class AuthRepository:IAuthRepository
    {
        private readonly AppDbContex _context;
        public AuthRepository(AppDbContex context) { _context = context; }

        public async Task<bool> UserExistsAsync(string email)
        {
            return await _context.users.AnyAsync(u => u.Email == email);
        }

        public async Task<User> GetUserByEmailAsync(string email)
        {
            return await _context.users.FirstOrDefaultAsync(u => u.Email == email);
        }

        public async Task CreateUserAsync(User user)
        {
            _context.users.Add(user);
            await _context.SaveChangesAsync();
        }
    }
}
