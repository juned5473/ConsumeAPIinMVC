using System.ComponentModel.DataAnnotations;

namespace KTMVCAPP1.Models
{
    public class Employee
    {

        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage = "Please enter First Name")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Please enter Last Name")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Please enter your City")]
        public string City { get; set; }

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

