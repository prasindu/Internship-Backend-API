using System.ComponentModel.DataAnnotations;

namespace Test_Backend.Dto
{
    public class CreateUpdateCustomerDto
    {
        [Required(ErrorMessage = "Name is required")]
        [MaxLength(100, ErrorMessage = "Cannot exceed 100 characters")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Enter a correct email address")]
        public string Email { get; set; }

        [Required]
        [MaxLength(10, ErrorMessage = "Cannot exceed 10 characters")]
        public string Phone { get; set; }

        [Required]
        [MaxLength(250, ErrorMessage = "Cannot exceed 250 characters")]
        public string Address { get; set; }

        [Required]
        [MaxLength(10, ErrorMessage = "Cannot exceed 10 characters")]
        public string Status { get; set; }
    }
}
