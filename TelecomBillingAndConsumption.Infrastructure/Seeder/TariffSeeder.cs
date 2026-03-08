using Microsoft.EntityFrameworkCore;
using TelecomBillingAndConsumption.Data.Entities;
using TelecomBillingAndConsumption.Data.Helpers;
using TelecomBillingAndConsumption.Infrastructure.DatabaseConntection;

namespace TelecomBillingAndConsumption.Infrastructure.Seeder
{
    public static class TariffSeeder
    {
        public static async Task SeedAsync(ApplicationDbContext context)
        {
            if (await context.TariffRules.AnyAsync())
                return;

            var tariffs = new List<TariffRule>
            {
                // CALLS
                new TariffRule
                {
                    UsageType = UsageType.Call,
                    IsRoaming = false,
                    IsPeak = false,
                    PricePerUnit = 0.05m
                },
                new TariffRule
                {
                    UsageType = UsageType.Call,
                    IsRoaming = false,
                    IsPeak = true,
                    PricePerUnit = 0.15m
                },
                new TariffRule
                {
                    UsageType = UsageType.Call,
                    IsRoaming = true,
                    IsPeak = false,
                    PricePerUnit = 0.15m
                },
                new TariffRule
                {
                    UsageType = UsageType.Call,
                    IsRoaming = true,
                    IsPeak = true,
                    PricePerUnit = 0.15m
                },

                // DATA
                new TariffRule
                {
                    UsageType = UsageType.Data,
                    IsRoaming = false,
                    IsPeak = false,
                    PricePerUnit = 0.05m
                },
                new TariffRule
                {
                    UsageType = UsageType.Data,
                    IsRoaming = true,
                    IsPeak = false,
                    PricePerUnit = 0.20m
                },

                // SMS
                new TariffRule
                {
                    UsageType = UsageType.SMS,
                    IsRoaming = false,
                    IsPeak = false,
                    PricePerUnit = 0.02m
                },
                new TariffRule
                {
                    UsageType = UsageType.SMS,
                    IsRoaming = true,
                    IsPeak = false,
                    PricePerUnit = 0.10m
                }
            };

            await context.TariffRules.AddRangeAsync(tariffs);
            await context.SaveChangesAsync();
        }
    }
}
