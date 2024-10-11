using Cabinet_Prototype.DTOs.FacultyDTOs;
using Cabinet_Prototype.Services.FacultyService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Cabinet_Prototype.Controllers
{
    [Route("api/faculty")]
    [ApiController]
    public class FacultyController:ControllerBase
    {
        private readonly IFacultyService _facultyService;

        public FacultyController (IFacultyService facultyService)
        {
            _facultyService = facultyService;
        }

        /// <summary>
        /// Create faculty
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> AddFaculty(FacultyDTO model)
        {
            try
            {
                var claim = User.FindFirst(ClaimTypes.Authentication);

                if (claim == null)
                {
                    return StatusCode(401, "please login first");
                }

                var res = await _facultyService.AddFaculty(model);

                return Ok(res);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }


        /// <summary>
        /// Get faculty list
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetFacultyList()
        {
            try
            {
                var claim = User.FindFirst(ClaimTypes.Authentication);

                if (claim == null)
                {
                    return StatusCode(401, "please login first");
                }

                var res = await _facultyService.ShowFacultyList();

                return Ok(res);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        /// <summary>
        /// Get faculty by id
        /// </summary>
        /// <param name="FacultyId"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("{FacultyId}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetFacultyById(Guid FacultyId)
        {
            try
            {
                var claim = User.FindFirst(ClaimTypes.Authentication);

                if (claim == null)
                {
                    return StatusCode(401, "please login first");
                }

                var res = await _facultyService.ShowFacultyById(FacultyId);

                return Ok(res);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        /// <summary>
        /// Edit faculty
        /// </summary>
        /// <param name="FacultyId"></param>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("{FacultyId}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> ChangeFacultyById(Guid FacultyId, FacultyDTO model)
        {
            try
            {
                var claim = User.FindFirst(ClaimTypes.Authentication);

                if (claim == null)
                {
                    return StatusCode(401, "please login first");
                }

                var res = await _facultyService.ChangeFacultyById(FacultyId, model);

                return Ok(res);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        /// <summary>
        /// Delete faculty
        /// </summary>
        /// <param name="FacultyId"></param>
        /// <returns></returns>
        [HttpDelete]
        [Route("{FacultyId}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteFacultyById(Guid FacultyId)
        {
            try
            {
                var claim = User.FindFirst(ClaimTypes.Authentication);

                if (claim == null)
                {
                    return StatusCode(401, "please login first");
                }

                var res = await _facultyService.DeleteFacultyById(FacultyId);

                return Ok(res);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }


    }
}
