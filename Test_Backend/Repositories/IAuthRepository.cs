using Test_Backend.Model;

namespace Test_Backend.Repositories
{
    public interface IAuthRepository
    {
        Task<bool> UserExistsAsync(string email);
        Task<User> GetUserByEmailAsync(string email);
        Task CreateUserAsync(User user);
    }
}
