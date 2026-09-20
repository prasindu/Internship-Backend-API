using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
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
        private readonly IConfiguration _config;
        public AuthController(AppDbContex contex, IConfiguration config)
        {
            _Contex = contex;
            _config = config;
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

            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.Name),
                new Claim(ClaimTypes.Email, user.Email)
            };

            var token = new JwtSecurityToken(
                issuer: _config["Jwt:Issuer"],
                audience: _config["Jwt:Audience"],
                claims: claims,
                expires: DateTime.Now.AddHours(1),
                signingCredentials: credentials);

            var jwt = new JwtSecurityTokenHandler().WriteToken(token);

            
            return Ok(new { token = jwt, massage = "login succesfull", name = user.Name });
        }
    }
}
