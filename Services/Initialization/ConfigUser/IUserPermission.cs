using Cabinet_Prototype.Models;

namespace Cabinet_Prototype.Services.Initialization.ConfigUser
{
    public interface IUserPermission
    {
        Task<User> GetPermissionAsync(string userId, string requiredRole);
    }
}
