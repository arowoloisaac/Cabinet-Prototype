using Cabinet_Prototype.DTOs.UserDTOs;
using Cabinet_Prototype.Models;
using Microsoft.AspNetCore.Mvc;

namespace Cabinet_Prototype.Services.SharedService
{
    public interface ISharedService
    {
        Task<TokenResonse> Login([FromBody] LoginDto loginDto);

        Task<string> ChangePassword(string oldPassword, string newPassword, string userId);

        Task<GetProfileDto> UserProfile(string Id);

        Task<string> UpdateUserProfile(UpdateProfileDto profileDto, string userId);
    }
}
