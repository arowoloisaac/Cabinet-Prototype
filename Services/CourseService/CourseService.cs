using Cabinet_Prototype.Data;
using Cabinet_Prototype.DTOs.CourseDTOs;
using Cabinet_Prototype.DTOs.DirectionDTOs;
using Cabinet_Prototype.DTOs.FacultyDTOs;
using Cabinet_Prototype.DTOs.GroupDTOs;
using Cabinet_Prototype.Enums;
using Cabinet_Prototype.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Cabinet_Prototype.Services.CourseService
{
    public class CourseService : ICourseService
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly UserManager<User> _userManager;

        public CourseService(ApplicationDbContext dbContext, UserManager<User> userManager)
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
                Semester = model.Semester,
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

        public async Task<CourseShowDTO> ShowCourseById(Guid courseId, string userId, List<string> roles)
        {
            IQueryable<Course> query = _dbContext.Courses;

            if (roles.Contains("teacher"))
            {
                // 教师只能看到他们教授的课程
                query = _dbContext.Courses.Where(c => c.CourseTeachers.Any(ct => ct.TeacherId.ToString() == userId));
            }
            else if (roles.Contains("student"))
            {
                // 学生只能看到他们所在组的课程
                query = _dbContext.Courses.Where(c => c.Group.StudentGroup.Any(s => s.StudentGroupId.ToString() == userId));
            }

            var course = await query
                .Where(f => f.Id == courseId)
                .Include(f => f.Group)
                .Include(f => f.CourseTeachers)
                    .ThenInclude(d => d.Teacher)
                .Select(f => new CourseShowDTO
                {
                    CourseId = f.Id,
                    Name = f.Name,
                    Description = f.Description,
                    Literature = f.Literature,
                    Reading = f.Reading,
                    Year = f.Year,
                    GroupId = f.GroupId,
                    Semester = f.Semester,
                    GroupName = f.Group.GroupNumber,
                    CourseTeachers = f.CourseTeachers.Select(d => new CourseTeacherShowDTO
                    {
                        TeacherId = d.TeacherId,
                        FirstName = d.Teacher.FirstName,
                        LastName = d.Teacher.LastName
                    }).ToList()
                })
                .FirstOrDefaultAsync();

            if (course == null)
            {
                throw new KeyNotFoundException($"No course found with ID: {courseId}");
            }

            return course;
        }



        public async Task<List<CourseShowDTO>> ShowAllCourses(string userId, List<string> roles, Semester? semesterFilter = null)
        {
            IQueryable<Course> query = _dbContext.Courses;

            if (roles.Contains("teacher"))
            {
                // 教师只能看到他们教授的课程
                query = query.Where(c => c.CourseTeachers.Any(ct => ct.TeacherId.ToString() == userId));
            }
            else if (roles.Contains("student"))
            {
                // 学生只能看到他们所在组的课程
                query = query.Where(c => c.Group.StudentGroup.Any(s => s.StudentGroupId.ToString() == userId));
            }

            // 添加对学期的可选筛选
            if (semesterFilter.HasValue)
            {
                query = query.Where(c => c.Semester == semesterFilter.Value);
            }

            var courses = await query
                .Include(f => f.Group)
                .Include(f => f.CourseTeachers)
                    .ThenInclude(d => d.Teacher)
                .Select(f => new CourseShowDTO
                {
                    CourseId = f.Id,
                    Name = f.Name,
                    Description = f.Description,
                    Literature = f.Literature,
                    Reading = f.Reading,
                    Year = f.Year,
                    GroupId = f.GroupId,
                    Semester = f.Semester,
                    GroupName = f.Group.GroupNumber,
                    CourseTeachers = f.CourseTeachers.Select(d => new CourseTeacherShowDTO
                    {
                        TeacherId = d.TeacherId,
                        FirstName = d.Teacher.FirstName,
                        LastName = d.Teacher.LastName
                    }).ToList()
                })
                .ToListAsync();

            return courses;
        }


        public async Task<List<CourseShowDTO>> AdminShowAllCourses()
        {
            var courses = await _dbContext.Courses
                .Include(f => f.Group)
                .Include(f => f.CourseTeachers)
                    .ThenInclude(d => d.Teacher)
                .Select(f => new CourseShowDTO
                {
                    CourseId = f.Id,
                    Name = f.Name,
                    Description = f.Description,
                    Literature = f.Literature,
                    Reading = f.Reading,
                    Year = f.Year,
                    GroupId = f.GroupId,
                    GroupName = f.Group.GroupNumber,
                    CourseTeachers = f.CourseTeachers.Select(d => new CourseTeacherShowDTO
                    {
                        TeacherId = d.TeacherId,
                        FirstName = d.Teacher.FirstName,
                        LastName = d.Teacher.LastName
                    }).ToList()
                })
                .ToListAsync(); // Retrieve all courses as a List

            return courses;
        }


        public async Task<CourseShowDTO> AdminShowCourseById(Guid CourseId)
        {
            var course = await _dbContext.Courses
               .Where(f => f.Id == CourseId)
               .Include(f => f.Group)
               .Include(f => f.CourseTeachers)
                   .ThenInclude(d => d.Teacher)
               .Select(f => new CourseShowDTO
               {
                   CourseId = f.Id,
                   Name = f.Name,
                   Description = f.Description,
                   Literature = f.Literature,
                   Reading = f.Reading,
                   Year = f.Year,
                   GroupId = f.GroupId,
                   GroupName = f.Group.GroupNumber,
                   CourseTeachers = f.CourseTeachers.Select(d => new CourseTeacherShowDTO
                   {
                       TeacherId = d.TeacherId,
                       FirstName = d.Teacher.FirstName,
                       LastName = d.Teacher.LastName
                   }).ToList()
               })
               .FirstOrDefaultAsync(); // Retrieve the first or default faculty

            if (course == null)
            {
                throw new KeyNotFoundException($"No faculty found with ID: {CourseId}");
            }

            return course;
        }

    }
}

