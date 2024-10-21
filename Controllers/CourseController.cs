using Cabinet_Prototype.DTOs.CourseDTOs;
using Cabinet_Prototype.DTOs.DirectionDTOs;
using Cabinet_Prototype.Enums;
using Cabinet_Prototype.Services.CourseService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Cabinet_Prototype.Controllers
{
    [Route("api/course")]
    [ApiController]
    public class CourseController: ControllerBase
    {
        private readonly ICourseService _courseService;

        public CourseController(ICourseService courseService)
        {
            _courseService = courseService;
        }


        /// <summary>
        /// add course
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> AddCourse(CourseDTO model)
        {
            try
            {
                var claim = User.FindFirst(ClaimTypes.Authentication);

                if (claim == null)
                {
                    return StatusCode(401, "please login first");
                }

                var res = await _courseService.AddCourse(model);

                return Ok(res);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        /// <summary>
        /// show course by id (if user is teacher, then show all he teaches course, if user is student, show all course with his group)
        /// </summary>
        /// <param name="courseId"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("{courseId}")]
        public async Task<IActionResult> ShowCourseById(Guid courseId)
        {
            try
            {
                // 获取用户ID
                var userId = User.FindFirst(ClaimTypes.Authentication)?.Value;

                // 尝试获取所有角色
                var roles = User.FindAll(ClaimTypes.Role).Select(r => r.Value).ToList();

                if (string.IsNullOrEmpty(userId) || !roles.Any())
                {
                    return StatusCode(401, "Please login first.");
                }

                var res = await _courseService.ShowCourseById(courseId, userId, roles);

                return Ok(res);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        /// <summary>
        /// show all course (if user is teacher, then show all he teaches course, if user is student, show all course with his group)
        /// </summary>
        /// <param name="semesterFilter"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("")]
        public async Task<IActionResult> ShowAllCourses(Semester? semesterFilter = null)
        {
            try
            {
                // 获取用户ID
                var userId = User.FindFirst(ClaimTypes.Authentication)?.Value;

                // 尝试获取所有角色
                var roles = User.FindAll(ClaimTypes.Role).Select(r => r.Value).ToList();

                if (string.IsNullOrEmpty(userId) || !roles.Any())
                {
                    return StatusCode(401, "Please login first.");
                }

                var res = await _courseService.ShowAllCourses(userId, roles, semesterFilter);

                return Ok(res);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        /// <summary>
        /// admin show all courses
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("admin")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> AdminShowAllCourses()
        {
            try
            {
                var claim = User.FindFirst(ClaimTypes.Authentication);

                if (claim == null)
                {
                    return StatusCode(401, "please login first");
                }

                var res = await _courseService.AdminShowAllCourses();

                return Ok(res);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        /// <summary>
        /// admin show course by id
        /// </summary>
        /// <param name="CourseId"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("{CourseId}/admin")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> AdminShowCourseById(Guid CourseId)
        {
            try
            {
                var claim = User.FindFirst(ClaimTypes.Authentication);

                if (claim == null)
                {
                    return StatusCode(401, "please login first");
                }

                var res = await _courseService.AdminShowCourseById(CourseId);

                return Ok(res);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        /// <summary>
        /// add course teacher (only admin and the course teacher can add teacher in course)
        /// </summary>
        /// <param name="CourseId"></param>
        /// <param name="TeacherId"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("{TeacherId}")]
        public async Task<IActionResult> AddCourseTeacher(Guid CourseId, Guid TeacherId)
        {
            try
            {
                // 获取用户ID
                var userId = User.FindFirst(ClaimTypes.Authentication)?.Value;

                // 尝试获取所有角色
                var roles = User.FindAll(ClaimTypes.Role).Select(r => r.Value).ToList();

                if (string.IsNullOrEmpty(userId) || !roles.Any())
                {
                    return StatusCode(401, "Please login first.");
                }

                var res = await _courseService.AddCourseTeacher(userId, roles, CourseId, TeacherId);

                return Ok(res);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        /// <summary>
        /// delete course teacher (only admin and the course teacher can delete teacher in course, and teacher cannot delete himself)
        /// </summary>
        /// <param name="CourseId"></param>
        /// <param name="TeacherId"></param>
        /// <returns></returns>
        [HttpDelete]
        [Route("{TeacherId}")]
        public async Task<IActionResult> DeleteCourseTeacher(Guid CourseId, Guid TeacherId)
        {
            try
            {
                // 获取用户ID
                var userId = User.FindFirst(ClaimTypes.Authentication)?.Value;

                // 尝试获取所有角色
                var roles = User.FindAll(ClaimTypes.Role).Select(r => r.Value).ToList();

                if (string.IsNullOrEmpty(userId) || !roles.Any())
                {
                    return StatusCode(401, "Please login first.");
                }

                var res = await _courseService.DeleteCourseTeacher(userId, roles, CourseId, TeacherId);

                return Ok(res);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        /// <summary>
        /// edit course
        /// </summary>
        /// <param name="CourseId"></param>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("{CourseId}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> EditCourse(Guid CourseId, CourseEditDTO model)
        {
            try
            {
                // 获取用户ID
                var userId = User.FindFirst(ClaimTypes.Authentication)?.Value;

                // 尝试获取所有角色
                var roles = User.FindAll(ClaimTypes.Role).Select(r => r.Value).ToList();

                if (string.IsNullOrEmpty(userId) || !roles.Any())
                {
                    return StatusCode(401, "Please login first.");
                }

                var res = await _courseService.EditCourse(userId, roles, CourseId, model);

                return Ok(res);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        /// <summary>
        /// delete course
        /// </summary>
        /// <param name="CourseId"></param>
        /// <returns></returns>
        [HttpDelete]
        [Route("{CourseId}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteCourse(Guid CourseId)
        {
            try
            {
                var claim = User.FindFirst(ClaimTypes.Authentication);

                if (claim == null)
                {
                    return StatusCode(401, "please login first");
                }

                var res = await _courseService.DeleteCourse(CourseId);

                return Ok(res);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }


    }
}
