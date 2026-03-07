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
            var count = await context.TariffRules.CountAsync();

            if (count > 0)
                return;

            var tariffs = new List<TariffRule>
            {
                new TariffRule
                {
                    Id = 1,
                    UsageType = UsageType.Call,
                    IsRoaming = true,
                    IsPeak = false,
                    PricePerUnit = 0.1500m
                },
                new TariffRule
                {
                    Id = 6,
                    UsageType = UsageType.Call,
                    IsRoaming = false,
                    IsPeak = false,
                    PricePerUnit = 0.0500m
                },
                new TariffRule
                {
                    Id = 8,
                    UsageType = UsageType.Data,
                    IsRoaming = false,
                    IsPeak = false,
                    PricePerUnit = 0.0500m
                },
                new TariffRule
                {
                    Id = 9,
                    UsageType = UsageType.Data,
                    IsRoaming = true,
                    IsPeak = false,
                    PricePerUnit = 0.2000m
                },
                new TariffRule
                {
                    Id = 10,
                    UsageType = UsageType.SMS,
                    IsRoaming = false,
                    IsPeak = false,
                    PricePerUnit = 0.0200m
                },
                new TariffRule
                {
                    Id = 11,
                    UsageType = UsageType.SMS,
                    IsRoaming = true,
                    IsPeak = false,
                    PricePerUnit = 0.0200m
                }
            };

            await context.TariffRules.AddRangeAsync(tariffs);
            await context.SaveChangesAsync();
        }
    }
}
