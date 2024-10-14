using Cabinet_Prototype.DTOs.DirectionDTOs;
using Cabinet_Prototype.DTOs.FacultyDTOs;
using Cabinet_Prototype.Services.DirectionService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Cabinet_Prototype.Controllers
{
    [Route("api/direction")]
    [ApiController]
    public class DirectionController: ControllerBase
    {
        private readonly IDirectionService _directionService;

        public DirectionController(IDirectionService directionService)
        {
            _directionService = directionService;
        }

        /// <summary>
        /// add direction
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> AddDirection(DirectionDTO model)
        {
            try
            {
                var claim = User.FindFirst(ClaimTypes.Authentication);

                if (claim == null)
                {
                    return StatusCode(401, "please login first");
                }

                var res = await _directionService.AddDirection(model);

                return Ok(res);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        /// <summary>
        /// view all direction in choose faculty
        /// </summary>
        /// <param name="FacultyId"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("list/{FacultyId}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> ShowDirectionList(Guid FacultyId)
        {
            try
            {
                var claim = User.FindFirst(ClaimTypes.Authentication);

                if (claim == null)
                {
                    return StatusCode(401, "please login first");
                }

                var res = await _directionService.ShowDirectionList(FacultyId);

                return Ok(res);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        /// <summary>
        /// show direction by id
        /// </summary>
        /// <param name="DirectionId"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("{DirectionId}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> ShowDirectionById(Guid DirectionId)
        {
            try
            {
                var claim = User.FindFirst(ClaimTypes.Authentication);

                if (claim == null)
                {
                    return StatusCode(401, "please login first");
                }

                var res = await _directionService.ShowDirectionById(DirectionId);

                return Ok(res);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }


        /// <summary>
        /// change direction by id
        /// </summary>
        /// <param name="DirectionId"></param>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("{DirectionId}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> ChangeDirectionById(Guid DirectionId, DirectionDTO model)
        {
            try
            {
                var claim = User.FindFirst(ClaimTypes.Authentication);

                if (claim == null)
                {
                    return StatusCode(401, "please login first");
                }

                var res = await _directionService.EditDirectionById(DirectionId, model);

                return Ok(res);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        /// <summary>
        /// delete direction by id
        /// </summary>
        /// <param name="DirectionId"></param>
        /// <returns></returns>
        [HttpDelete]
        [Route("{DirectionId}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteDirectionById(Guid DirectionId)
        {
            try
            {
                var claim = User.FindFirst(ClaimTypes.Authentication);

                if (claim == null)
                {
                    return StatusCode(401, "please login first");
                }

                var res = await _directionService.DeleteDirectionById(DirectionId);

                return Ok(res);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

    }
}
