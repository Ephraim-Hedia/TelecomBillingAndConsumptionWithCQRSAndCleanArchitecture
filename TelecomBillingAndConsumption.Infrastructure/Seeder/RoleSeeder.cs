using Microsoft.AspNetCore.Identity;
using TelecomBillingAndConsumption.Data.Entities.Identity;

namespace TelecomBillingAndConsumption.Infrastructure.Seeder
{
    public static class RoleSeeder
    {
        public static async Task SeedAsync(RoleManager<Role> _roleManager)
        {
            var roles = new[] { "Admin", "User" };

            foreach (var roleName in roles)
            {
                var exists = await _roleManager.RoleExistsAsync(roleName);

                if (!exists)
                {
                    await _roleManager.CreateAsync(new Role
                    {
                        Name = roleName
                    });
                }
            }
        }

    }
}
