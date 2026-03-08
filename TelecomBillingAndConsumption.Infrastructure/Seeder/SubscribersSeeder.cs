using Microsoft.EntityFrameworkCore;
using TelecomBillingAndConsumption.Data.Entities;
using TelecomBillingAndConsumption.Infrastructure.DatabaseConntection;

namespace TelecomBillingAndConsumption.Infrastructure.Seeder
{
    public static class SubscribersSeeder
    {
        public static async Task SeedAsync(ApplicationDbContext context)
        {
            if (await context.Subscribers.AnyAsync())
                return;

            var users = await context.Users
                .Where(u => u.UserName.StartsWith("user"))
                .OrderBy(u => u.Id)
                .Take(10)
                .ToListAsync();

            var plans = await context.Plans.ToListAsync();

            var subscribers = new List<Subscriber>();

            for (int i = 0; i < users.Count; i++)
            {
                subscribers.Add(new Subscriber
                {
                    Name = users[i].FullName,
                    UserId = users[i].Id,

                    PhoneNumber = $"201000000{i + 1:D2}",

                    Country = "Egypt",

                    PlanId = plans[i % plans.Count].Id,

                    IsRoaming = i % 4 == 0,

                    SubscriptionStartDate = DateTime.UtcNow.AddYears(-(i % 4)),

                    IsActive = true
                });
            }

            await context.Subscribers.AddRangeAsync(subscribers);
            await context.SaveChangesAsync();
        }
    }
}