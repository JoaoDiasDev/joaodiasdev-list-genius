using ListGenius.Domain.Context;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;

namespace ListGenius.CrossCutting.Seeds
{
    public static class SeedIdentity
    {
        public static async Task SeedRolesAndUsersAsync(IServiceProvider serviceProvider)
        {
            var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            var userManager = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();

            // Define roles
            var roles = new[] { "Admin", "User" };

            foreach (var role in roles)
            {
                if (!await roleManager.RoleExistsAsync(role))
                {
                    await roleManager.CreateAsync(new IdentityRole(role));
                }
            }

            // Define users
            var adminEmail = "joaodiasworking@gmail.com";
            var userEmail = "jmmatheus23@gmail.com";

            if (await userManager.FindByEmailAsync(adminEmail) == null)
            {
                var adminUser = new ApplicationUser
                {
                    UserName = adminEmail,
                    Email = adminEmail,
                    EmailConfirmed = true
                };

                await userManager.CreateAsync(adminUser, "kOtehazu321*r");
                await userManager.AddToRoleAsync(adminUser, "Admin");
            }

            if (await userManager.FindByEmailAsync(userEmail) == null)
            {
                var normalUser = new ApplicationUser
                {
                    UserName = userEmail,
                    Email = userEmail,
                    EmailConfirmed = true
                };

                await userManager.CreateAsync(normalUser, "Matheus321*");
                await userManager.AddToRoleAsync(normalUser, "User");
            }
        }
    }
}
