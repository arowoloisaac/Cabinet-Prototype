using Cabinet_Prototype.Configurations;
using Cabinet_Prototype.Models;
using Microsoft.AspNetCore.Identity;

namespace Cabinet_Prototype.Services.Initialization
{
    public static class RoleConfiguration
    {
        public static async Task ConfigureRole(this WebApplication app)
        { 
            using var scope = app.Services.CreateScope();

            var roles = new[]
            {
                ApplicationRoleName.Teacher,
                ApplicationRoleName.Student,
                ApplicationRoleName.Admin
            };

            var roleCreator = scope.ServiceProvider.GetRequiredService<RoleManager<Role>>();

            foreach (var role in roles)
            {
                await roleCreator.CreateAsync(new Role
                {
                    Name = role
                });
            }
        }
    }
}
