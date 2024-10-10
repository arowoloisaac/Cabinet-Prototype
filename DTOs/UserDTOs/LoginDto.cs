using System.ComponentModel.DataAnnotations;

namespace Cabinet_Prototype.DTOs.UserDTOs
{
    public class LoginDto
    {
        [EmailAddress]
        public string Email { get; set; } = string.Empty;

        public string Password { get; set; } = string.Empty;
    }
}
