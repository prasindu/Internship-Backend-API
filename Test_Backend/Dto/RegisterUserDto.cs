using System.ComponentModel.DataAnnotations;

namespace Test_Backend.Dto
{
    public class RegisterUserDto
    {
        [Required(ErrorMessage = "Name is required")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Enter a correct email")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Enter password")]
        [MinLength(6, ErrorMessage = "Password must be at least 6 characters")]
        public string Password { get; set; }
    }
}
