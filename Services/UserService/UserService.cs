using Cabinet_Prototype.Data;
using Cabinet_Prototype.DTOs.UserDTOs;
using Cabinet_Prototype.Enums;
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
                StudentFacultyId = dto.StudentFacultyId,
                StudentGroupId = dto.StudentGroupId,
                StudentGrade = dto.StudentGrade,
                UserType = dto.UserType,
                StudentDirectionId = dto.StudentDirectionId,
                BirthDate = dto.BirthDate,
                PhoneNumber = dto.PhoneNumber,
                RegisteredTime = DateTime.UtcNow,
                RequestStatus = RequestStatus.isPending,
            });
            
            await _dbContext.SaveChangesAsync();

            return ("awaits administrator authorization");
        }

        public async Task<List<TeacherDTO>> GetAllTeachers()
        {
            var users = await _userManager.Users.ToListAsync();

            var teachers = new List<TeacherDTO>();
            foreach (var user in users)
            {
                if (await _userManager.IsInRoleAsync(user, "Teacher"))
                {
                    teachers.Add(new TeacherDTO
                    {
                        Id = user.Id,
                        Name = $"{user.FirstName} {user.LastName}"
                    });
                }
            }

            return teachers;
        }


    }
}
