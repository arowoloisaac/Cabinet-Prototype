using Azure.Core;
using Cabinet_Prototype.Configurations;
using Cabinet_Prototype.DTOs.UserDTOs;
using Cabinet_Prototype.Models;
using Cabinet_Prototype.Services.TokenService;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace Cabinet_Prototype.Services.SharedService
{
    public class SharedService : ISharedService
    {
        private readonly UserManager<User> _userManager;
        private readonly JwtBearerSetting _jwtToken;
        private readonly ITokenGenerator _tokenGenerator;

        public SharedService(UserManager<User> userManager, IOptions<JwtBearerSetting> jwtOptions, ITokenGenerator tokenGenerator)
        {
            _userManager = userManager;
            _jwtToken = jwtOptions.Value;
            _tokenGenerator = tokenGenerator;
        }

        /**public Task<User> AuthentifyUser(string userId, string roles)
        {
            throw new NotImplementedException();
        }**/

        public async Task<TokenResonse> Login(LoginDto loginDto)
        {
            var user = await ValidateUser(loginDto);

            if (user == null)
            {
                throw new InvalidOperationException("Login Fail");
            }

            var token = _tokenGenerator.GenerateToken(user, await _userManager.GetRolesAsync(user));

            return new TokenResonse(token);
        }


        public async Task<GetProfileDto> UserProfile(string userId)
        {
            var findUser = await _userManager.FindByIdAsync(userId);

            if (findUser == null)
            {
                throw new Exception("yo, you don't even have a profile");
            }

            else
            {
                var user = new GetProfileDto { FirstName = findUser.FirstName, LastName = findUser.LastName, Email = findUser.Email};

                return user;
            }
        }

        public async Task<string> UpdateUserProfile(UpdateProfileDto updateProfileDto, string userId)
        {
            var identifyUser = await _userManager.FindByIdAsync(userId);

            if (identifyUser == null)
            {
                throw new Exception("User not found");
            }

            else
            {
                object value;
                value = updateProfileDto.FirstName == null ? identifyUser.FirstName : identifyUser.FirstName == updateProfileDto.FirstName;
                //identifyUser.FirstName = updateProfileDto.FirstName;
                value = updateProfileDto.LastName == null ? identifyUser.LastName : identifyUser.LastName == updateProfileDto.LastName;

                value = updateProfileDto.PhoneNumber == null ? identifyUser.PhoneNumber : identifyUser.PhoneNumber = updateProfileDto.PhoneNumber;

                value = updateProfileDto.BirthDate == null ? identifyUser.BirthDate : identifyUser.BirthDate == updateProfileDto.BirthDate;
                //identifyUser.BirthDate = updateProfileDto.BirthDate;
                //identifyUser.Avatar = updateProfileDto.ProfilePicture;

                var updateUser = await _userManager.UpdateAsync(identifyUser);

                if (!updateUser.Succeeded)
                {
                    throw new Exception("Unable to save user");
                }
                return $"User with email has been saved {identifyUser.Email}";
            }
        }


        private async Task<User> ValidateUser(LoginDto request)
        {
            var identifyUser = await _userManager.FindByEmailAsync(request.Email);

            if (identifyUser != null)
            {
                var result = _userManager.PasswordHasher.VerifyHashedPassword(identifyUser, identifyUser.PasswordHash, request.Password);

                return result == PasswordVerificationResult.Success ? identifyUser : null;
            }
            throw new Exception("Can't find user");
        }
    }
}
