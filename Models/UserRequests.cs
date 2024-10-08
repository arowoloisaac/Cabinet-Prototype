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

        public FacultyType StudentFaculty { get; set; }

        public string StudentDirection { get; set; } = string.Empty;

        public string StudentGroup {  get; set; } = string.Empty;

        public string StudentGrade { get; set; } = string.Empty;
        // public ImageModel? Image { get; set; }
        //public string Faculty {  get; set; } = string.Empty;

        public bool isApproved { get; set; } = false;

        //public Guid GroupId { get; set; }

        public UserType UserType { get; set; }
    }
    /***
     * in the service i have to create a function that retrieve faculties, direction and group***/
}
