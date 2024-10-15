using Cabinet_Prototype.DTOs.CourseDTOs;

namespace Cabinet_Prototype.Services.CourseService
{
    public interface ICourseService
    {
        Task<Guid> AddCourse(CourseDTO model);
    }
}
