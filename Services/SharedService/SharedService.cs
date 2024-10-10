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
