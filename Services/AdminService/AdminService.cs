﻿using Azure;
using Cabinet_Prototype.Configurations;
using Cabinet_Prototype.Data;
using Cabinet_Prototype.DTOs.UserDTOs;
using Cabinet_Prototype.Enums;
using Cabinet_Prototype.Models;
using Cabinet_Prototype.Services.EmailService;
using Cabinet_Prototype.Services.Initialization.PasswordGenerator;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace Cabinet_Prototype.Services.AdminService
{
    public class AdminService : IAdminService
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly UserManager<User> _userManager;
        private readonly IPasswordGen _passwordGen;
        private readonly IEmailService _emailService;
        private int passwordLength = 9;

        public AdminService(ApplicationDbContext dbContext, UserManager<User> userManager, 
            IPasswordGen passwordGen, IEmailService emailService)
        {
            _dbContext = dbContext;
            _userManager = userManager;
            _passwordGen = passwordGen;
            _emailService = emailService;
        }

        public async Task<Message> AddUser(Guid RequestId)
        {
            var searchRequest = await _dbContext.UserRequests.Where(_ => _.Id ==RequestId && !_.isRejected).SingleOrDefaultAsync();

            if (searchRequest == null)
            {
                return new Message($"request with id - {RequestId} not found");
            }

            else
            {
                var user = new User {
                    Id = Guid.NewGuid(),
                    FirstName = searchRequest.FirstName,
                    LastName = searchRequest.LastName,
                    Email = searchRequest.Email,
                    BirthDate = searchRequest.BirthDate,
                    PhoneNumber = searchRequest.PhoneNumber,
                    UserName = searchRequest.Email
                };

                if (searchRequest.UserType == UserType.Student)
                {
                    user.StudentFacultyId.Id = searchRequest.StudentFacultyId;
                    user.StudentDirection.Id = searchRequest.StudentDirectionId;
                    user.StudentGroupId.Id = searchRequest.StudentGroupId;

                    user.UserType = UserType.Student;

                }

                else
                {
                    //continue 
                    user.UserType = UserType.Teacher;
                }

                string generateUserPassword = _passwordGen.GeneratePassword(passwordLength);

                var savedUser = await _userManager.CreateAsync(user, generateUserPassword);

                if (!savedUser.Succeeded)
                {
                    throw new Exception("Unable to save user to the database");
                }

                else
                {
                    var getCreatedUser = await _userManager.FindByEmailAsync(user.Email);

                    string subject = "Your Cabinet Account Password";
                    string description = $"<h3>Hello {getCreatedUser.FirstName} {getCreatedUser.LastName} </h3>/n" +
                        "We noticed that you created an account with your credientials intact, to validate your account for perfect security and also assured that you created " +
                        "the account." +
                        $"Your password: {generateUserPassword}";


                    await _emailService.SendEmail(getCreatedUser.Email, subject, description);

                    await InitialUserToRole(getCreatedUser);

                    searchRequest.isApproved = true;
                    searchRequest.isAccepted = true;
                }

                await _dbContext.SaveChangesAsync();

                return new Message("Done with task");
            }
        }

        public async Task<List<UserRequestDto>> GetRequests(string adminId)
        {
            var findUser = await _userManager.FindByIdAsync(adminId);

            if (findUser != null)
            {
                var requests = await _dbContext.UserRequests.Where(filter => filter.isApproved== false).ToListAsync();

                if (requests == null)
                {
                    return new List<UserRequestDto>();
                }

                //await _emailService.SendEmail("arowoloisaac01@gmail.com", "null", "null");

                var responseList = requests.Select(request => new UserRequestDto
                {
                    Id = request.Id,
                    Email = request.Email,
                }).ToList();

                return responseList;

            }
            throw new Exception("Can not perform this action");
        }

        
        public async Task<Message> DenyUser(Guid requestId)
        {
            var findRequestId = await _dbContext.UserRequests.Where(_ =>  _.Id == requestId && _.isApproved == false).SingleOrDefaultAsync();

            if (findRequestId != null)
            {
                findRequestId.isApproved = false;
                findRequestId.isAccepted = false;
                findRequestId.isRejected = true;

                _dbContext.SaveChanges();

                return new Message("User successfully denied");
            }
            throw new Exception("Unable to process this function");
        }



        public async Task<Message> AddUserToRole(Guid userId, UserType role)
        {
            //might initialize admin user later on

            var findUser = await _userManager.FindByIdAsync(userId.ToString());

            if (findUser != null)
            {
                if (!await _userManager.IsInRoleAsync(findUser, role.ToString()))
                {
                    var response = await _userManager.AddToRoleAsync(findUser, role.ToString());

                    if (response.Succeeded)
                    {
                        return new Message($"role added for user: {findUser.UserName} with the role: {role}");
                    }
                    else
                    {
                        return new Message($"Unable to add user: {findUser.UserName} to the role {role}");
                    }
                }
                else
                {
                    throw new Exception("User already exist with same role");
                }
            }
            else
            {
                throw new Exception("Can't find the user in the database");
            }
        }



        public async Task<Message> RemoveUserFromRole(Guid userId, UserType role)
        {
            //might initialize admin user later on

            var findUser = await _userManager.FindByIdAsync(userId.ToString());

            if (findUser != null)
            {
                if (!await _userManager.IsInRoleAsync(findUser, role.ToString()))
                {
                    return new Message($"user {findUser.Email} does not have this role {role}");
                }
                else
                {
                    var response = await _userManager.RemoveFromRoleAsync(findUser, role.ToString());

                    if (response.Succeeded)
                    {
                        return new Message($"user: {findUser.UserName} has been removed from the role: {role}");
                    }
                    else
                    {
                        return new Message($"Unable to remove user: {findUser.UserName} to the role {role}");
                    }
                }
            }
            else
            {
                throw new Exception("Can't find the user in the database");
            }
        }


        public async Task<List<GetUsersDto>> GetAllUsers()
        {
            var users = await _userManager.Users.Where(getMails => getMails != null).ToListAsync();

            if (users == null)
            {
                return new List<GetUsersDto>();
            }
            else
            {
                var response = users.Select(users => new GetUsersDto
                {
                    userId = users.Id,
                    Email = users.Email
                }).ToList();

                return response;
            }
        }


        private async Task<Message> InitialUserToRole(User getCreatedUser)
        {
            if (getCreatedUser != null)
            {
                if (getCreatedUser.UserType == UserType.Student)
                {
                    var addRoleStudent = await _userManager.AddToRoleAsync(getCreatedUser, ApplicationRoleName.Student);

                    if (addRoleStudent.Succeeded)
                    {
                        return new Message($"role added for user: {getCreatedUser.UserName} with the role: {ApplicationRoleName.Student}");
                    }
                    else
                    {
                        return new Message($"Unable to add user: {getCreatedUser.UserName} to the role {ApplicationRoleName.Student}");
                    }
                }
                else if (getCreatedUser.UserType == UserType.Teacher)
                {
                    var addRoleTeacher = await _userManager.AddToRoleAsync(getCreatedUser, ApplicationRoleName.Teacher);

                    if (addRoleTeacher.Succeeded)
                    {
                        return new Message($"role added for user: {getCreatedUser.UserName} with the role: {ApplicationRoleName.Student}");
                    }
                    else
                    {
                        return new Message($"Unable to add user: {getCreatedUser.UserName} to the role {ApplicationRoleName.Student}");
                    }
                }

                else
                {
                    throw new Exception("You have to be either student or teacher");
                }
            }
            else
            {
                throw new Exception($"Unable to find user {getCreatedUser.Email} in the database");
            }
        }
    }
}
