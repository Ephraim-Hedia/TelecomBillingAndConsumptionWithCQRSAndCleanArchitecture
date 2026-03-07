using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using TelecomBillingAndConsumption.Data.Entities.Identity;

namespace TelecomBillingAndConsumption.Infrastructure.Seeder
{
    public static class UserSeeder
    {
        public static async Task SeedAsync(UserManager<User> _userManager)
        {
            var usersCount = await _userManager.Users.CountAsync();
            if (usersCount <= 0)
            {
                var defaultuser = new User()
                {
                    UserName = "admin",
                    Email = "admin@project.com",
                    FullName = "TelecomBillilngAndConsumption",
                    Country = "Egypt",
                    PhoneNumber = "01210404274",
                    Address = "Egypt",
                    EmailConfirmed = true,
                    PhoneNumberConfirmed = true
                };
                await _userManager.CreateAsync(defaultuser, "Test123$");
                await _userManager.AddToRoleAsync(defaultuser, "Admin");
            }
        }
    }
}
