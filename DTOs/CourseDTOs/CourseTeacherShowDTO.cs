namespace Cabinet_Prototype.DTOs.CourseDTOs
{
    public class CourseTeacherShowDTO
    {
        public Guid TeacherId { get; set; }

        public string FirstName { get; set; } = string.Empty;

        public string LastName { get; set; } = string.Empty;
    }
}