using Cabinet_Prototype.Enums;
using Cabinet_Prototype.Models;

namespace Cabinet_Prototype.DTOs.CourseDTOs
{
    public class CourseDTO
    {

        public string Name { get; set; } = string.Empty;

        public string Description { get; set; } = string.Empty;

        public string Literature { get; set; } = string.Empty;

        public string Reading { get; set; } = string.Empty;

        public string Year { get; set; } = string.Empty;

<<<<<<< HEAD
        public string Semester { get; set; } = string.Empty;
=======
        public Semester Semester { get; set; }
>>>>>>> dc0e8aa32f27588518d3ed71c4f1e891d9673395

        public Guid GroupId { get; set; }

        public List<CourseTeacherDTO> CourseTeachers { get; set; }
    }
}
