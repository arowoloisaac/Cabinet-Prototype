using Cabinet_Prototype.DTOs.GroupDTOs;
using Cabinet_Prototype.DTOs.ScheduleDTOs;
using Cabinet_Prototype.Services.GroupService;
using Cabinet_Prototype.Services.ScheduleSerives;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Cabinet_Prototype.Controllers
{
    [Route("api/schedule")]
    [ApiController]
    public class ScheduleController : ControllerBase
    {
        private readonly IScheduleService _scheduleService;

        public ScheduleController(IScheduleService scheduleService)
        {
            _scheduleService = scheduleService;
        }

        /// <summary>
        /// add schedule
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> AddSchedule(CreateScheduleDTO model)
        {
            try
            {
                var claim = User.FindFirst(ClaimTypes.Authentication);

                if (claim == null)
                {
                    return StatusCode(401, "please login first");
                }

                var res = await _scheduleService.CreateSchedule(model);

                return Ok(res);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        /// <summary>
        /// edit schedule
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> EditSchedule(ShowScheduleDTO model)
        {
            try
            {
                var claim = User.FindFirst(ClaimTypes.Authentication);

                if (claim == null)
                {
                    return StatusCode(401, "please login first");
                }

                var res = await _scheduleService.EditSchedule(model);

                return Ok(res);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        /// <summary>
        /// delete schedule
        /// </summary>
        /// <param name="scheduleId"></param>
        /// <returns></returns>
        [HttpDelete]
        [Route("{scheduleId}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteSchedule(Guid scheduleId)
        {
            try
            {
                var claim = User.FindFirst(ClaimTypes.Authentication);

                if (claim == null)
                {
                    return StatusCode(401, "please login first");
                }

                var res = await _scheduleService.DeleteSchedule(scheduleId);

                return Ok(res);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        /// <summary>
        /// show schedule by student group
        /// </summary>
        /// <param name="studentId"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("stuednt")]
        [Authorize]
        public async Task<IActionResult> ShowScheduleByStudentGroup()
        {
            try
            {
                var claim = User.FindFirst(ClaimTypes.Authentication);

                if (claim == null)
                {
                    return StatusCode(401, "please login first");
                }

                var userId = Guid.Parse(claim.Value);

                var res = await _scheduleService.ShowScheduleByStudentGroup(userId);

                return Ok(res);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        /// <summary>
        /// show schedule by course id
        /// </summary>
        /// <param name="courseId"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("course/{courseId}")]
        [Authorize]
        public async Task<IActionResult> ShowScheduleByCourse(Guid courseId)
        {
            try
            {
                var claim = User.FindFirst(ClaimTypes.Authentication);

                if (claim == null)
                {
                    return StatusCode(401, "please login first");
                }

                var res = await _scheduleService.ShowScheduleByCourse(courseId);

                return Ok(res);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        /// <summary>
        /// show schudle by teacher id
        /// </summary>
        /// <param name="teacherId"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("teacher/{teacherId}")]
        [Authorize]
        public async Task<IActionResult> ShowScheduleByTeacherId(Guid teacherId)
        {
            try
            {
                var claim = User.FindFirst(ClaimTypes.Authentication);

                if (claim == null)
                {
                    return StatusCode(401, "please login first");
                }

                var res = await _scheduleService.ShowScheduleByTeacher(teacherId);

                return Ok(res);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        /// <summary>
        /// show schudle by user is teacher 
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("teacher")]
        [Authorize(Roles = "Teacher")]
        public async Task<IActionResult> ShowScheduleByTeacher()
        {
            try
            {
                var claim = User.FindFirst(ClaimTypes.Authentication);

                if (claim == null)
                {
                    return StatusCode(401, "please login first");
                }
                var userId = Guid.Parse(claim.Value);

                var res = await _scheduleService.ShowScheduleByTeacher(userId);

                return Ok(res);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}
