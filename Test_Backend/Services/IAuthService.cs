using Test_Backend.Dto;


namespace Test_Backend.Services
{
    public interface IAuthService
    {
        Task<string> RegisterAsync(RegisterUserDto dto);
        Task<AuthResponseDto> LoginAsync(LoginUserDto dto);
    }
}
