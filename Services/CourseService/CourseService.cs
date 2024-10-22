using Cabinet_Prototype.Data;
using Cabinet_Prototype.DTOs.CourseDTOs;
using Cabinet_Prototype.DTOs.DirectionDTOs;
using Cabinet_Prototype.DTOs.FacultyDTOs;
using Cabinet_Prototype.DTOs.GroupDTOs;
using Cabinet_Prototype.Enums;
using Cabinet_Prototype.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
using System.Data;

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

            if (roles.Contains("Teacher"))
            {
                // Teacher only can see there teachs course
                query = _dbContext.Courses.Where(c => c.CourseTeachers.Any(ct => ct.TeacherId.ToString() == userId));
            }
            else if (roles.Contains("Student"))
            {
                // Students only can see the course belong their group
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

            if (roles.Contains("Teacher"))
            {
                query = query.Where(c => c.CourseTeachers.Any(ct => ct.TeacherId.ToString() == userId));
            }
            else if (roles.Contains("Student"))
            {
                query = query.Where(c => c.Group.StudentGroup.Any(s => s.StudentGroupId.ToString() == userId));
            }

            // add querty of semester
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
                .ToListAsync(); 

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
               .FirstOrDefaultAsync();

            if (course == null)
            {
                throw new KeyNotFoundException($"No faculty found with ID: {CourseId}");
            }

            return course;
        }

        public async Task<Message> AddCourseTeacher(string userId, List<string> roles, Guid courseId, Guid teacherId)
        {
            var courseTeachers = await _dbContext.CourseTeachers
                                         .Where(ct => ct.CourseId == courseId)
                                         .ToListAsync();

            if (roles.Contains("Admin") || courseTeachers.Any(ct => ct.TeacherId.ToString() == userId))
            {
                if (courseTeachers.Any(ct => ct.TeacherId == teacherId))
                {
                    throw new InvalidOperationException ("Teacher already added to the course.");
                }
                else
                {
                    var newCourseTeacher = new CourseTeacher
                    {
                        CourseId = courseId,
                        TeacherId = teacherId
                    };
                    _dbContext.CourseTeachers.Add(newCourseTeacher);
                    await _dbContext.SaveChangesAsync();
                    return new Message ($"Teacher {teacherId} added successfully.");
                }
            }
            else
            {
                throw new UnauthorizedAccessException ("Do not have permission to add teachers to this course.");
            }
        }

        public async Task<Message> DeleteCourseTeacher(string userId, List<string> roles, Guid courseId, Guid teacherId)
        {
            var courseTeachers = await _dbContext.CourseTeachers
                                         .Where(ct => ct.CourseId == courseId)
                                         .ToListAsync();

            if(userId == teacherId.ToString())
            {
                throw new InvalidOperationException("Could not delete yourself");
            }

            if (roles.Contains("Admin") || courseTeachers.Any(ct => ct.TeacherId.ToString() == userId))
            {
                var teacherToDelete = courseTeachers.FirstOrDefault(ct => ct.TeacherId == teacherId);

                if (teacherToDelete == null)
                {
                    throw new KeyNotFoundException("The specified teacher is not assigned to this course.");
                }

                _dbContext.CourseTeachers.Remove(teacherToDelete);
                await _dbContext.SaveChangesAsync();

                return new Message ($"Teacher {teacherId} successfully removed from the course." );
            }
            else
            {
                throw new UnauthorizedAccessException("You do not have permission to remove teachers from this course.");
            }

        }


        public async Task<Message> EditCourse(string userId, List<string> roles, Guid courseId, CourseEditDTO model)
        {
            var course = await _dbContext.Courses
                                 .Include(c => c.CourseTeachers)
                                 .FirstOrDefaultAsync(c => c.Id == courseId);
            if (course == null)
            {
                throw new KeyNotFoundException($"Course with ID {courseId} not found.");
            }

            if (roles.Contains("Admin") || course.CourseTeachers.Any(ct => ct.TeacherId.ToString() == userId))
            {

                course.Name = model.Name;
                course.Description = model.Description;
                course.Literature = model.Literature;
                course.Reading = model.Reading;
                course.Year = model.Year;
                course.Semester = model.Semester;
                course.GroupId = model.GroupId;

                return new Message($"Course {courseId} successfully edit.");
            }
            else
            {
                throw new UnauthorizedAccessException("You do not have permission to edit course.");
            }

        }

        public async Task<Message> DeleteCourse(Guid courseId)
        {
            var course = await _dbContext.Courses
                                 .Include(c => c.CourseTeachers)
                                 .FirstOrDefaultAsync(c => c.Id == courseId);
            if (course == null)
            {
                throw new KeyNotFoundException($"Course with ID {courseId} not found.");
            }

            foreach (var teacher in course.CourseTeachers.ToList())
            {
                _dbContext.CourseTeachers.Remove(teacher);
            }

            _dbContext.Courses.Remove(course);

            await _dbContext.SaveChangesAsync();

            return new Message($"Course {courseId} successfully deleted.");
        }



    }
}

