using Cabinet_Prototype.DTOs.CourseDTOs;
using Cabinet_Prototype.Enums;
using Cabinet_Prototype.Models;

namespace Cabinet_Prototype.Services.CourseService
{
    public interface ICourseService
    {
        Task<Guid> AddCourse(CourseDTO model);

        Task<CourseShowDTO> ShowCourseById(Guid courseId, string userId, List<string> roles);

        Task<List<CourseShowDTO>> ShowAllCourses(string userId, List<string> roles, Semester? semesterFilter = null);

        Task<List<CourseShowDTO>> AdminShowAllCourses();

        Task<CourseShowDTO> AdminShowCourseById(Guid CourseId);

        Task<Message> AddCourseTeacher(string userId, List<string> roles, Guid courseId, Guid teacherId);

        Task<Message> DeleteCourseTeacher(string userId, List<string> roles, Guid courseId, Guid teacherId);

        Task<Message> EditCourse(string userId, List<string> roles, Guid courseId, CourseEditDTO model);

        Task<Message> DeleteCourse(Guid courseId);


    }
}
