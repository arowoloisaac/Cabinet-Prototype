namespace Cabinet_Prototype.Models
{
    public class Result
    {
        public Guid Id { get; set; }

        public float CourseGrade { get; set; }

        public Guid CourseId { get; set; }

        public Guid StudentId { get; set; }
    }
}
