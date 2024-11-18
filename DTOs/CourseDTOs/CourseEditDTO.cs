using Cabinet_Prototype.Enums;

namespace Cabinet_Prototype.DTOs.CourseDTOs
{
    public class CourseEditDTO
    {
        public string Name { get; set; } = string.Empty;

        public string Description { get; set; } = string.Empty;

        public string Literature { get; set; } = string.Empty;

        public string Reading { get; set; } = string.Empty;

        public string Year { get; set; } = string.Empty;

        public Semester Semester { get; set; }

        public Guid GroupId { get; set; }
    }
}
