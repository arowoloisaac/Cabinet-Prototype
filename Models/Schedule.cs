using Cabinet_Prototype.Enums;

namespace Cabinet_Prototype.Models
{
    public class Schedule
    {
        public Guid Id { get; set; }

        //we can use the dto to manipulate the data here, in the same for creating fields for course name and teacher

        //this to extract both time and day 
        public DateTime ClassTime { get; set; }

        public string? Location { get; set; } 

        public string? ClassNumber { get; set; }

        public ClassFormat Format { get; set; }

        public Guid CourseId { get; set; }

        public Guid GroupsId { get; set; }

        public Course? Course { get; set; }
        
        public Group? Groups { get; set; }

        public Guid TeacherId { get; set; }
    }
}
