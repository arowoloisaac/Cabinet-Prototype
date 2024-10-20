using Cabinet_Prototype.Enums;
using System.ComponentModel.DataAnnotations;

namespace Cabinet_Prototype.DTOs.UserDTOs
{
    public class RegisterDto
    {
        public string FirstName { get; set; } = string.Empty;

        public string LastName { get; set; } = string.Empty;

        public DateOnly BirthDate { get; set; }

        [EmailAddress]
        public string Email { get; set; } = string.Empty;

        [Phone]
        [MaxLength(13)]
        public string PhoneNumber { get; set; } = string.Empty;

        public UserType UserType { get; set; }

        public FacultyType StudentFaculty { get; set; }

        //check for the direction in the service, must be in the faculty
        public string StudentDirection { get; set; } = string.Empty;

        //check for the group in the service, must be in the direction
        public ulong StudentGroup { get; set; } 

        //[MaxLength(4)] //here we will work with regex
        public string StudentGrade { get; set; } = string.Empty;

        public IFormFile? ProfilePicture { get; set; }
        // public ImageModel? Image { get; set; }
        //public string Faculty {  get; set; } = string.Empty;

        
    }
}
