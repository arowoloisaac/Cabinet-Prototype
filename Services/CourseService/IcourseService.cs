using Cabinet_Prototype.DTOs.CourseDTOs;

namespace Cabinet_Prototype.Services.CourseService
{
    public interface ICourseService
    {
        Task<Guid> AddCourse(CourseDTO model);

        Task<CourseShowDTO> ShowCourseById(Guid courseId, string userId, List<string> roles);

        Task<List<CourseShowDTO>> ShowAllCourses();
    }
}
