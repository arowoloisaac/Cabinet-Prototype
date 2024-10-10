using Azure;
using Cabinet_Prototype.Configurations;
using Cabinet_Prototype.Data;
using Cabinet_Prototype.DTOs.UserDTOs;
using Cabinet_Prototype.Enums;
using Cabinet_Prototype.Models;
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

        private int passwordLength = 9;

        public AdminService(ApplicationDbContext dbContext, UserManager<User> userManager, IPasswordGen passwordGen)
        {
            _dbContext = dbContext;
            _userManager = userManager;
            _passwordGen = passwordGen;
        }

        public async Task<Message> AddUser(Guid RequestId)
        {
            var searchRequest = await _dbContext.UserRequests.FindAsync(RequestId);

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
                    var getFaculty = await _dbContext.Faculties
                        .SingleOrDefaultAsync(faculty => faculty.Name.ToLower() == searchRequest.StudentFaculty.ToString().ToLower());

                    var getDirection = await _dbContext.Directions
                        .SingleOrDefaultAsync(faculty => faculty.Name.ToLower() == searchRequest.StudentDirection.ToLower());

                    var getGroup = await _dbContext.Groups
                        .SingleOrDefaultAsync(faculty => faculty.GroupNumber == searchRequest.StudentGroupNumber);

                    //set faculty id
                    user.StudentFacultyId = getFaculty;
                    user.StudentDirection = getDirection;
                    user.StudentGroupId = getGroup;
                }

                else
                {
                    //continue 
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

                    await InitialUserToRole(getCreatedUser);

                    searchRequest.isApproved = true;
                }

                await _dbContext.SaveChangesAsync();

                return new Message("Done with task");
                



                //update the userRequest table by setting the isApproved to true

                // messaging queue for the user to receive their password
            }
        }

        public async Task<List<UserRequestDto>> GetRequests(string adminId)
        {
            var findUser = await _userManager.FindByIdAsync(adminId);

            if (findUser != null)
            {
                var requests = await _dbContext.UserRequests.Where(filter => !filter.isApproved).ToListAsync();

                if (requests == null)
                {
                    return new List<UserRequestDto>();
                }

                var responseList = requests.Select(request => new UserRequestDto
                {
                    Id = request.Id,
                    Email = request.Email,
                }).ToList();

                return responseList;

            }
            throw new Exception("Can not perform this action");
        }

        public async Task<Message> AddUserToRole(Guid userId)
        {
            throw new NotImplementedException();
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
