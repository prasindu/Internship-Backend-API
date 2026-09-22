using Microsoft.AspNetCore.Mvc;
using Test_Backend.Services;
using Test_Backend.Dto;

namespace Test_Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterUserDto dto)
        {
            var errorMessage = await _authService.RegisterAsync(dto);

            if (errorMessage != null)
            {
                return BadRequest(errorMessage);
            }

            return Ok(new { massage = "registration successful" });
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginUserDto dto)
        {
            var response = await _authService.LoginAsync(dto);

            if (response == null)
            {
                return Unauthorized("invalid email or password");
            }

            return Ok(new { token = response.Token, massage = response.Message, name = response.Name });
        }
    }
}