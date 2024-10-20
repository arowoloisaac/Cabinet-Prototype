using Cabinet_Prototype.Configurations;
using Cabinet_Prototype.Enums;
using Cabinet_Prototype.Services.AdminService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.Security.Claims;

namespace Cabinet_Prototype.Controllers
{
    [Route("api/")]
    [ApiController]
    [Authorize(Roles =ApplicationRoleName.Admin)]
    public class AdminController : ControllerBase
    {
        private readonly IAdminService _adminService;

        public AdminController(IAdminService adminService)
        {
            _adminService = adminService;
        }


        [HttpGet]
        [Route("get-requests")]
        public async Task<IActionResult> GetRequest()
        {
            try
            {
                var claimUserAdmin = User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Authentication);

                if (claimUserAdmin == null)
                {
                    return NotFound("User not found");
                }
                else
                {
                    return Ok(await _adminService.GetRequests(claimUserAdmin.Value));
                }
            }
            catch
            { 
                return BadRequest();
            }
        }


        [HttpGet]
        [Route("users")]
        public async Task<IActionResult> GetUsers()
        {
            try
            {
                return Ok(await _adminService.GetAllUsers());
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        [HttpPost]
        [Route("add-user/{requestId}")]
        public async Task<IActionResult> AddUser([Required] Guid requestId)
        {
            try
            {
                return Ok(await _adminService.AddUser(requestId));
            }
            catch
            {
                return BadRequest();
            }
        }


        [HttpPut]
        [Route("deny-user/{requestId}")]
        public async Task<IActionResult> DenyUser([Required] Guid requestId)
        {
            try
            {
                return Ok(await _adminService.DenyUser(requestId));
            }
            catch
            {
                return BadRequest();
            }
        }


        [HttpPost]
        [Route("role/add/{userId}/{role}")]
        public async Task<IActionResult> AddUserToRole(Guid userId, UserType role)
        {
            try
            {
                return Ok(await _adminService.AddUserToRole(userId, role));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        [HttpDelete]
        [Route("role/remove/{userId}/{role}")]
        public async Task<IActionResult> RemoveUserFromRole(Guid userId, UserType role)
        {
            try
            {
                return Ok(await _adminService.RemoveUserFromRole(userId, role));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
