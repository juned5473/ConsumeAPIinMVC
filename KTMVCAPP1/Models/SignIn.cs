using System.ComponentModel.DataAnnotations;

namespace KTMVCAPP1.Models
{
    public class SignIn
    {
        [Key]

        [Required(ErrorMessage = "Please enter your Email")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Required(ErrorMessage = "Please enter your Password")]
        [DataType(DataType.Password)]
        [MaxLength(15, ErrorMessage = "Password cannot be greater than 15 characters")]
        [MinLength(6, ErrorMessage = "Password cannot be less than 6 characters")]
        public string Password { get; set; }
    }
}
