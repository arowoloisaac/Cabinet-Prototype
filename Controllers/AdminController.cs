using Cabinet_Prototype.Configurations;
using Cabinet_Prototype.Services.AdminService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Cabinet_Prototype.Controllers
{
    [Route("api/admin")]
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
            throw new NotImplementedException();
        }
    }
}
