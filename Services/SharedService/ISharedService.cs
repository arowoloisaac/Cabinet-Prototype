using Cabinet_Prototype.DTOs.UserDTOs;
using Cabinet_Prototype.Models;
using Microsoft.AspNetCore.Mvc;

namespace Cabinet_Prototype.Services.SharedService
{
    public interface ISharedService
    {
        Task<TokenResonse> Login([FromBody] LoginDto loginDto);

        //Task<User> AuthentifyUser(string userId, string roles);

        Task<GetProfileDto> UserProfile(string Id);
    }
}
