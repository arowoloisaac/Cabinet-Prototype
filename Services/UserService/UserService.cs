using Cabinet_Prototype.Data;
using Cabinet_Prototype.DTOs.UserDTOs;
using Cabinet_Prototype.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Cabinet_Prototype.Services.UserService
{
    public class UserService : IUserService
    {
        private readonly UserManager<User> _userManager;
        private readonly ApplicationDbContext _dbContext;

        public UserService(UserManager<User> userManager, ApplicationDbContext context)
        {
            _userManager = userManager;
            _dbContext = context;
        }

        public async Task<string> RegisterUser(RegisterDto dto)
        {
            var findUser = await _dbContext.UserRequests.SingleOrDefaultAsync(findUserEmail => findUserEmail.Email == dto.Email);

            if (findUser != null)
            {
                throw new Exception("User already exist in the database");
            }

            // birthdate must be at least 15 years back
            var user = await _dbContext.UserRequests.AddAsync( new UserRequests
            {
                Email = dto.Email,
                FirstName = dto.FirstName,
                LastName = dto.LastName,
                Id = Guid.NewGuid(),
                StudentFaculty = dto.StudentFaculty,
                StudentGroupNumber = dto.StudentGroup,
                StudentGrade = dto.StudentGrade,
                UserType = dto.UserType,
                StudentDirection = dto.StudentDirection,
                BirthDate = dto.BirthDate,
                PhoneNumber = dto.PhoneNumber,
            });
            
            await _dbContext.SaveChangesAsync();

            return ("awaits administrator authorization");
        }
    }
}
