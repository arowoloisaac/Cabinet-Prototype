using Cabinet_Prototype.DTOs;
using Cabinet_Prototype.Models;
using Cabinet_Prototype.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Cabinet_Prototype.Controllers
{
    [Route("api/user")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private IUserService _userService;
        private UserManager<User> _userManager;

        public UserController(IUserService userService, UserManager<User> userManager)
        {
            _userService = userService;
            _userManager = userManager;
        }

        /// <summary>
        /// register new user
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("register")]
        [ProducesResponseType(typeof(ResponseModelDTO), 200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(typeof(ResponseModelDTO), 500)]
        public async Task<IActionResult> Register([FromBody] RegisterModelDTO model)
        {
            try
            {
                //var Response = await _userService.Register(model);
                return Ok(Response);
            }
            catch (ArgumentException ex)
            {
                var errorResponse = new ResponseModelDTO
                {
                    Status = "Error",
                    Message = ex.Message
                };
                return BadRequest(errorResponse);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new ResponseModelDTO { Status = "Error", Message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new ResponseModelDTO { Status = "Error", Message = ex.Message });
            }
        }

    }
}
