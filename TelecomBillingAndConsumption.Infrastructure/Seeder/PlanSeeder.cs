using Microsoft.EntityFrameworkCore;
using TelecomBillingAndConsumption.Data.Entities;
using TelecomBillingAndConsumption.Infrastructure.DatabaseConntection;

namespace TelecomBillingAndConsumption.Infrastructure.Seeder
{
    public static class PlansSeeder
    {
        public static async Task SeedAsync(ApplicationDbContext context)
        {
            if (await context.Plans.AnyAsync())
                return;

            var plans = new List<Plan>
            {
                new Plan
                {
                    Name = "Basic Plan",
                    MonthlyFee = 50,
                    IncludedCallMinutes = 200,
                    IncludedDataMB = 2048, // 2 GB
                    IncludedSMS = 100,
                    IsActive = true
                },

                new Plan
                {
                    Name = "Standard Plan",
                    MonthlyFee = 100,
                    IncludedCallMinutes = 500,
                    IncludedDataMB = 5120, // 5 GB
                    IncludedSMS = 250,
                    IsActive = true
                },

                new Plan
                {
                    Name = "Premium Plan",
                    MonthlyFee = 200,
                    IncludedCallMinutes = 1000,
                    IncludedDataMB = 10240, // 10 GB
                    IncludedSMS = 500,
                    IsActive = true
                }
            };

            await context.Plans.AddRangeAsync(plans);
            await context.SaveChangesAsync();
        }
    }
}