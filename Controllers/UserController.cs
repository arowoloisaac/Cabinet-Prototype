using Cabinet_Prototype.DTOs.UserDTOs;
using Cabinet_Prototype.Services.UserService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Cabinet_Prototype.Controllers
{
    [Route("api/user")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> RegisterUser(RegisterDto register)
        {
            var registerUser = await _userService.RegisterUser(register);
            return Ok(registerUser);
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        [Route("teacher")]
        public async Task<IActionResult> GetTeacherList()
        {
            var registerUser = await _userService.GetAllTeachers();
            return Ok(registerUser);
        }


    }
}
