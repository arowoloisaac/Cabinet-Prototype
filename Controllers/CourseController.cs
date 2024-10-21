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

        [HttpGet]
        [Route("")]
        public async Task<IActionResult> ShowCourses(Semester? semesterFilter = null)
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

        [HttpPost]
        [Route("admin")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> AdminAddCourse()
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

        [HttpPost]
        [Route("{CourseId}/admin")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> AdminAddCourse(Guid CourseId)
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
    }
}
