using Microsoft.AspNetCore.Identity;
using TelecomBillingAndConsumption.Data.Entities.Identity;

namespace TelecomBillingAndConsumption.Infrastructure.Seeder
{
    public static class UsersSeeder
    {
        public static async Task SeedAsync(UserManager<User> userManager)
        {
            // Admin User
            var adminEmail = "admin@telecom.com";

            var admin = await userManager.FindByEmailAsync(adminEmail);

            if (admin == null)
            {
                admin = new User
                {
                    UserName = "admin",
                    Email = adminEmail,
                    EmailConfirmed = true,
                    FullName = "System Admin",
                    Address = "Cairo",
                    Country = "Egypt"
                };

                await userManager.CreateAsync(admin, "Admin@123");
                await userManager.AddToRoleAsync(admin, "Admin");
            }

            // 10 Telecom Users
            for (int i = 1; i <= 10; i++)
            {
                var email = $"user{i}@telecom.com";

                var user = await userManager.FindByEmailAsync(email);

                if (user != null)
                    continue;

                user = new User
                {
                    UserName = $"user{i}",
                    Email = email,
                    EmailConfirmed = true,
                    FullName = $"Telecom User {i}",
                    Address = "Cairo",
                    Country = "Egypt"
                };

                await userManager.CreateAsync(user, "User@123");
                await userManager.AddToRoleAsync(user, "User");
            }
        }
    }
}