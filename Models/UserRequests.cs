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

        public Guid StudentDirectionId { get; set; }

        public Guid StudentGroupId { get; set; }

        public string StudentGrade { get; set; } = string.Empty;

        public bool? isApproved { get; set; } = false;

        public bool isAccepted { get; set; } = false; 

        public bool isRejected { get; set; }

        public UserType UserType { get; set; }

        public DateTime? RegisteredTime { get; set; }

        public DateTime? RejectedTime { get; set; }

        public RequestStatus RequestStatus { get; set; }
    }
}
