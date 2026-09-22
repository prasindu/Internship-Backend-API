using System.ComponentModel.DataAnnotations;

namespace Test_Backend.Dto
{
    public class LoginUserDto
    {
        [Required(ErrorMessage = "Email is required")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Password is required")]
        public string Password { get; set; }
    }
}
