using Cabinet_Prototype.Models;
using Microsoft.AspNetCore.Identity;

namespace Cabinet_Prototype.Services.Initialization.ConfigUser
{
    public class UserPermission : IUserPermission
    {
        private readonly UserManager<User> _userManager;

        public UserPermission(UserManager<User> userManager)
        {
            _userManager = userManager;
        }

        public async Task<User> GetPermissionAsync(string userId, string requiredRole)
        {
            var user = await _userManager.FindByIdAsync(userId);

            if (user == null)
            {
                throw new UnauthorizedAccessException("User is not logged in or registered");
            }

            if (!await _userManager.IsInRoleAsync(user, requiredRole))
            {
                throw new UnauthorizedAccessException($"User is not in role {requiredRole}");
            }
            return user;
        }
    }
}
