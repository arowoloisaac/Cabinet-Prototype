using Azure.Core;
using Cabinet_Prototype.Configurations;
using Cabinet_Prototype.Data;
using Cabinet_Prototype.DTOs.UserDTOs;
using Cabinet_Prototype.Enums;
using Cabinet_Prototype.Models;
using Cabinet_Prototype.Services.EmailService;
using Cabinet_Prototype.Services.Initialization.PasswordGenerator;
using Cabinet_Prototype.Services.TokenService;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace Cabinet_Prototype.Services.SharedService
{
    public class SharedService : ISharedService
    {
        private readonly UserManager<User> _userManager;
        private readonly JwtBearerSetting _jwtToken;
        private readonly ITokenGenerator _tokenGenerator;
        private readonly IPasswordGen _passwordGen;
        private readonly IEmailService _emailService;
        private int passwordLength = 9;

        public SharedService(UserManager<User> userManager, IOptions<JwtBearerSetting> jwtOptions, ITokenGenerator tokenGenerator, 
            IPasswordGen passwordGen, IEmailService emailService)
        {
            _userManager = userManager;
            _jwtToken = jwtOptions.Value;
            _tokenGenerator = tokenGenerator;
            _passwordGen = passwordGen;
            _emailService = emailService;
        }

        public async Task<string> ChangePassword(string oldPassword, string newPassword, string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);

            if (user == null)
            {
                throw new ArgumentNullException("User doesn't exist, try registering");
            }
            else
            {
                if (oldPassword != null && await _userManager.CheckPasswordAsync(user, oldPassword))
                {
                    var result = await _userManager.ChangePasswordAsync(user, oldPassword, newPassword);

                    if (!result.Succeeded)
                    {
                        throw new Exception("unable to save password, system error");
                    }

                    return "New passord successful saved";
                }
                throw new Exception(" Password doesn't match");
            }
        }

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

                value = updateProfileDto.LastName == null ? identifyUser.LastName : identifyUser.LastName == updateProfileDto.LastName;

                value = updateProfileDto.PhoneNumber == null ? identifyUser.PhoneNumber : identifyUser.PhoneNumber = updateProfileDto.PhoneNumber;

                value = updateProfileDto.BirthDate == null ? identifyUser.BirthDate : identifyUser.BirthDate == updateProfileDto.BirthDate;

                var updateUser = await _userManager.UpdateAsync(identifyUser);

                if (!updateUser.Succeeded)
                {
                    throw new Exception("Unable to save user");
                }
                return $"User with email has been saved {identifyUser.Email}";
            }
        }

        public async Task<Message> ForgotPassword(string email)
        { 
            var userExit = await _userManager.FindByEmailAsync(email);

            if (userExit == null)
            {
                throw new KeyNotFoundException("Could not find this email");
            }

            string generateUserPassword = _passwordGen.GeneratePassword(passwordLength);

            var resetToken = await _userManager.GeneratePasswordResetTokenAsync(userExit);
            var resetPasswordResult = await _userManager.ResetPasswordAsync(userExit, resetToken, generateUserPassword);

            if (!resetPasswordResult.Succeeded)
            {
                throw new Exception($"Failed to reset password: {string.Join(", ", resetPasswordResult.Errors.Select(e => e.Description))}");
            }

            string subject = "Your Cabinet Account Password";
            string description = $"<h3>Hello {userExit.FirstName} {userExit.LastName} </h3>/n" +
                    "We noticed that you created an account with your credientials intact, to validate your account for perfect security and also assured that you created " +
                    "the account." +
                    $"Your password: {generateUserPassword}";


            await _emailService.SendEmail(userExit.Email, subject, description);

            return new Message("new password has alerady sended, please check your email");
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
