using System.ComponentModel.DataAnnotations;

namespace Test_Backend.Dto
{
    public class CreateUpdateCustomerDto
    {
        [Required(ErrorMessage ="name is required..")]
        [MaxLength(100,ErrorMessage ="connod exceed 100 caractoers")]
        public string Name { get; set; }

        [Required(ErrorMessage ="Email is required..")]
        [EmailAddress(ErrorMessage ="enter the currect Email address")]
        public string Email { get; set; }

        [Required]
        [MaxLength(10,ErrorMessage = "connod exceed 10 caractoers")]
        public string Phone { get; set; }

        [Required]
        [MaxLength(250,ErrorMessage = "connod exceed 250 caractoers")]
        public string Address { get; set; }
        
        [Required]
        [MaxLength(10, ErrorMessage = "connod exceed 10 caractoers")]
        public string Status { get; set; }
    }
}
