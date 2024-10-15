using Cabinet_Prototype.Data;
using Cabinet_Prototype.DTOs.CourseDTOs;
using Cabinet_Prototype.DTOs.DirectionDTOs;
using Cabinet_Prototype.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Cabinet_Prototype.Services.CourseService
{
    public class CourseService: ICourseService
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly UserManager<User> _userManager;

        public CourseService (ApplicationDbContext dbContext, UserManager<User> userManager)
        {
            _dbContext = dbContext;
            _userManager = userManager;
        }

        public async Task<Guid> AddCourse(CourseDTO model)
        {
            foreach (var courseTeacher in model.CourseTeachers)
            {
                var user = await _userManager.FindByIdAsync(courseTeacher.TeacherId.ToString());
                if (user == null)
                {
                    throw new ArgumentException($"No user found with ID: {courseTeacher.TeacherId}");
                }

                var isTeacher = await _userManager.IsInRoleAsync(user, "Teacher");
                if (!isTeacher)
                {
                    throw new InvalidOperationException($"User with ID: {courseTeacher.TeacherId} is not a teacher");
                }
            }

            var course = new Course
            {
                Name = model.Name,
                Description = model.Description,
                Literature = model.Literature,
                Reading = model.Reading,
                Year = model.Year,
                GroupId = model.GroupId,
            };

            _dbContext.Courses.Add(course);

            foreach (var courseTeacher in model.CourseTeachers)
            {
                var newCourseTeacher = new CourseTeacher
                {
                    CourseId = course.Id,
                    TeacherId = courseTeacher.TeacherId
                };

                _dbContext.CourseTeachers.Add(newCourseTeacher);
            }

            await _dbContext.SaveChangesAsync();

            return course.Id;
        }

    }
}
