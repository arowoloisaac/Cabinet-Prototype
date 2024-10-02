using Cabinet_Prototype.Enums;
using System.ComponentModel.DataAnnotations;

namespace Cabinet_Prototype.DTOs
{
    public class RegisterModelDTO
    {
        [Required]
        [StringLength(1000, MinimumLength = 1)]
        public string Name { get; set; }

        [Required]
        [EmailAddress]
        [MinLength(1)]
        public string Email { get; set; }

        [Phone]
        public string PhoneNumber { get; set; }

        [Required]
        [MinLength(6)]
        public string Password { get; set; }

        public DateTime BirthDate { get; set; }

        [Required]
        public UserType UserType { get; set; }

        public string Avatar { get; set; }

        public string? GradeNumber { get; set; }


    }
}
