﻿using Cabinet_Prototype.DTOs.UserDTOs;
using Cabinet_Prototype.Services.SharedService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Client;
using System.Security.Claims;

namespace Cabinet_Prototype.Controllers
{
    [Route("api/user")]
    [ApiController]
    public class SharedController : ControllerBase
    {
        private readonly ISharedService _sharedService;

        public SharedController(ISharedService sharedService)
        {
            _sharedService = sharedService;
        }

        [HttpPost]
        [Route("login")]
        [AllowAnonymous]
        public async Task<IActionResult> Login([FromBody] LoginDto loginDto)
        {
            try
            {
                return Ok(await _sharedService.Login(loginDto));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        [HttpGet]
        [Route("profile")]
        [Authorize]
        public async Task<IActionResult> ViewProfile()
        {
            try
            {
                var getUser = User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Authentication);

                if (getUser != null)
                {
                    return Ok(await _sharedService.UserProfile(getUser.Value));
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            return Ok();
        }

    }
}