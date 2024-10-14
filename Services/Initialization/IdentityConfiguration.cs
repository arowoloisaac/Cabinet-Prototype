using Cabinet_Prototype.Configurations;
using Cabinet_Prototype.Models;
using Microsoft.AspNetCore.Identity;

namespace Cabinet_Prototype.Services.Initialization
{
    public static class IdentityConfiguration
    {
        public static async Task ConfigureAdmin(this WebApplication app)
        {
            using var serviceScope = app.Services.CreateScope();

            var userManager = serviceScope.ServiceProvider.GetService<UserManager<User>>();

            var config = app.Configuration.GetSection("AdminCreditials");

            var checkAdminExistence = await userManager.FindByEmailAsync(config["Email"]);

            if (checkAdminExistence == null)
            {
                var adminUser = new User
                {
                    FirstName = config["FirstName"],
                    LastName = config["LastName"],
                    Email = config["Email"],
                    UserName = config["Email"],
                    Password = config["Password"]
                };

                var result = await userManager.CreateAsync(adminUser, config["Password"]);

                if (!result.Succeeded)
                {
                    throw new Exception("Unable to create user");
                }

                checkAdminExistence = await userManager.FindByEmailAsync(config["Email"]);
            }

            if (!await userManager.IsInRoleAsync(checkAdminExistence, ApplicationRoleName.Admin))
            {
                await userManager.AddToRoleAsync(checkAdminExistence, ApplicationRoleName.Admin);
            }
        }
    }
}
