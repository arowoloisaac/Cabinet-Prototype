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
        
        public DateOnly BirthDate { get; set; }

        [EmailAddress]
        public string Email { get; set; } = string.Empty;

        [Phone]
        public string PhoneNumber { get; set; } = string.Empty;


        public Guid StudentFacultyId { get; set; }

        //check for the direction in the service, must be in the faculty
        public Guid StudentDirectionId { get; set; }

        //check for the group in the service, must be in the direction
        public Guid StudentGroupId { get; set; }

        //[MaxLength(4)] //here we will work with regex
        public string StudentGrade { get; set; } = string.Empty;

        // public ImageModel? Image { get; set; }
        //public string Faculty {  get; set; } = string.Empty;

        public bool? isApproved { get; set; } = false;

        public bool isAccepted { get; set; } = false; 

        public bool isRejected { get; set; }

        //public Guid GroupId { get; set; }

        public UserType UserType { get; set; }

        public byte[]? UserPicture { get; set; }
    }
    /***
     * in the service i have to create a function that retrieve faculties, direction and group***/
}
