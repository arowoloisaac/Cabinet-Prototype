namespace Cabinet_Prototype.Models
{
    public class CourseTeacher
    {
        public Guid Id { get; set; }

        public Guid CourseId { get; set; }

        public Guid TeacherId { get; set; }

        public User Teacher { get; set; }


    }
}
