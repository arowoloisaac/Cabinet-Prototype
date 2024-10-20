using Cabinet_Prototype.Enums;
using System.ComponentModel.DataAnnotations;

namespace Cabinet_Prototype.DTOs.UserDTOs
{
    public class UpdateProfileDto
    {
        public string FirstName { get; set; } = string.Empty;

        public string LastName { get; set; } = string.Empty;

        //  DateOnly date = new(2021, 1, 31);
        [DataType(DataType.Date)]
        public DateOnly BirthDate { get; set; }

        [Phone]
        [MaxLength(13)]
        public string PhoneNumber { get; set; } = string.Empty;

        public IFormFile ProfilePicture { get; set; }
    }
}
