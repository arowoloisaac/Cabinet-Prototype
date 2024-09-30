using Cabinet_Prototype.Enums;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Cabinet_Prototype.Models
{
    public class UserRequests
    {
        public Guid Id { get; set; }

        public string FirstName { get; set; } = string.Empty;

        public string LastName { get; set; } = string.Empty;
        
        public DateTime BirthDate { get; set; }

        [EmailAddress]
        public string Email { get; set; } = string.Empty;

        [Phone]
        public string PhoneNumber { get; set; } = string.Empty;

        public string Password { get; set; } = string.Empty;

       // public ImageModel? Image { get; set; }

        public UserType UserType { get; set; }
    }
}
