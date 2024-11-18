namespace Cabinet_Prototype.Models
{
    public class CourseTeacher
    {
        public Guid Id { get; set; }

        public Guid CourseId { get; set; }

        public Guid TeacherId { get; set; }

        public User Teacher { get; set; }
<<<<<<< HEAD
=======

>>>>>>> dc0e8aa32f27588518d3ed71c4f1e891d9673395

    }
}
