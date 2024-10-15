using Cabinet_Prototype.DTOs.CourseDTOs;
using Cabinet_Prototype.DTOs.DirectionDTOs;
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
    }
}
