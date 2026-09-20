using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Test_Backend.Data;
using Test_Backend.Model;
using test01.Dto;

namespace Test_Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly AppDbContex _Contex;
        public AuthController(AppDbContex contex)
        {
            _Contex = contex;
        }


        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterUserDto dto)
        {
            var userexit = await _Contex.users.AnyAsync(u => u.Email == dto.Email);
            if (userexit)
            {
                return BadRequest("email already add");
            }
            var newUser = new User
            {
                Name = dto.Name,
                Email = dto.Email,
                Password = dto.Password
            };

            _Contex.Add(newUser);
            await _Contex.SaveChangesAsync();
            return Ok(new { massage = "registration succussfull" });
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginUserDto dto)
        {
            var user = await _Contex.users.FirstOrDefaultAsync(u => u.Email == dto.Email && u.Password == dto.Password);
            if (user == null)
            {
                return Unauthorized("invalid email or password");
            }

            return Ok(new { massage = "login succesfull", name = user.Name });
        }
    }
}
