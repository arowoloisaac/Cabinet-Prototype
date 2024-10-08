using Cabinet_Prototype.Data;
using Cabinet_Prototype.DTOs.UserDTOs;
using Cabinet_Prototype.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Cabinet_Prototype.Services.AdminService
{
    public class AdminService : IAdminService
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly UserManager<User> _userManager;

        public AdminService(ApplicationDbContext dbContext, UserManager<User> userManager)
        {
            _dbContext = dbContext;
            _userManager = userManager;
        }

        public async Task<Message> AddUser(UserRequestDto userRequestDto)
        {
            throw new NotImplementedException();
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
    }
}
