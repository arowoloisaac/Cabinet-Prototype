using Cabinet_Prototype.DTOs.DirectionDTOs;
using Cabinet_Prototype.DTOs.FacultyDTOs;
using Cabinet_Prototype.DTOs.GroupDTOs;
using Cabinet_Prototype.Services.GroupService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Cabinet_Prototype.Controllers
{
    [Route("api/group")]
    [ApiController]
    public class GroupController:ControllerBase
    {
        private readonly IGroupService _groupService;

        public GroupController (IGroupService groupService)
        {
            _groupService = groupService;
        }

        /// <summary>
        /// add group
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> AddGroup(GroupDTO model)
        {
            try
            {
                var claim = User.FindFirst(ClaimTypes.Authentication);

                if (claim == null)
                {
                    return StatusCode(401, "please login first");
                }

                var res = await _groupService.AddGroup(model);

                return Ok(res);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }


        /// <summary>
        /// list group by direction id
        /// </summary>
        /// <param name="DirectionId"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("list/{DirectionId}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> ShowGroupList(Guid DirectionId)
        {
            try
            {
                var claim = User.FindFirst(ClaimTypes.Authentication);

                if (claim == null)
                {
                    return StatusCode(401, "please login first");
                }

                var res = await _groupService.ShowGroupList(DirectionId);

                return Ok(res);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }


        /// <summary>
        /// show group by id
        /// </summary>
        /// <param name="GroupId"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("{GroupId}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> ShowGroupById(Guid GroupId)
        {
            try
            {
                var claim = User.FindFirst(ClaimTypes.Authentication);

                if (claim == null)
                {
                    return StatusCode(401, "please login first");
                }

                var res = await _groupService.ShowGroupById(GroupId);

                return Ok(res);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        /// <summary>
        /// change group by id
        /// </summary>
        /// <param name="GroupId"></param>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("{GroupId}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> ChangeGroupById(Guid GroupId, GroupDTO model)
        {
            try
            {
                var claim = User.FindFirst(ClaimTypes.Authentication);

                if (claim == null)
                {
                    return StatusCode(401, "please login first");
                }

                var res = await _groupService.EditGroupById(GroupId, model);

                return Ok(res);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }


        /// <summary>
        /// delete group by id
        /// </summary>
        /// <param name="GroupId"></param>
        /// <returns></returns>
        [HttpDelete]
        [Route("{GroupId}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteGroupById(Guid GroupId)
        {
            try
            {
                var claim = User.FindFirst(ClaimTypes.Authentication);

                if (claim == null)
                {
                    return StatusCode(401, "please login first");
                }

                var res = await _groupService.DeleteGroupById(GroupId);

                return Ok(res);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}
