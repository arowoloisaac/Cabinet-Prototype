using Cabinet_Prototype.Enums;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace Cabinet_Prototype.Models
{
    public class User : IdentityUser<Guid>
    {
        public string FirstName { get; set; } = string.Empty;

        public string LastName { get; set; } = string.Empty;

        public DateTime BirthDate { get; set; }

        [Required]
        public UserType UserType { get; set; }

        public string Avatar { get; set; } = string.Empty;

        //this serves as the avatar
        //public ImageModel? Image { get; set; }

        public string? Password { get; set; }

        public string GradeNumber { get; set; } = string.Empty;

        public ICollection<Course>? Courses { get; set; }

        public ICollection<Result>? StudentResults { get; set; }

        public ICollection<Schedule>? Schedules { get; set; }

        public Group? StudentGroupId { get; set; }

        public Faculty? StudentFacultyId { get; set; }
    }


    /**public class ImageModel
    {
        public string ImageBase { get; set; } = string.Empty;

        public string FileType { get; set; } = string.Empty ;

        public string? FileName { get; set; }
    }**/
}
