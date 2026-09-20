using System.ComponentModel.DataAnnotations;

namespace test01.Dto
{
    public class RegisterUserDto
    {
        [Required (ErrorMessage =" name is reqired")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "enter correct email")]
        public string Email { get; set; }
        [Required(ErrorMessage ="enter password")]
        [MinLength(6, ErrorMessage = "Password must be at least 6 characters")]
        public string Password { get; set; }
    }
}
