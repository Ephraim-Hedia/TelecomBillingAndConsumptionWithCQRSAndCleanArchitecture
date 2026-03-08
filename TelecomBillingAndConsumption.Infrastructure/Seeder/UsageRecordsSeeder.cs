using Microsoft.EntityFrameworkCore;
using TelecomBillingAndConsumption.Data.Entities;
using TelecomBillingAndConsumption.Data.Helpers;
using TelecomBillingAndConsumption.Infrastructure.DatabaseConntection;

namespace TelecomBillingAndConsumption.Infrastructure.Seeder
{
    public static class UsageRecordsSeeder
    {
        public static async Task SeedAsync(ApplicationDbContext context)
        {
            if (await context.UsageRecords.AnyAsync())
                return;

            var subscribers = await context.Subscribers.ToListAsync();

            var usageRecords = new List<UsageRecord>();

            var random = new Random();

            foreach (var subscriber in subscribers)
            {
                for (int i = 0; i < 20; i++)
                {
                    var usageType = (UsageType)random.Next(1, 4);

                    var timestamp = DateTime.UtcNow
                        .AddDays(-random.Next(1, 25))
                        .AddHours(random.Next(0, 23));

                    bool isPeak = timestamp.Hour >= 8 && timestamp.Hour < 20;

                    var record = new UsageRecord
                    {
                        SubscriberId = subscriber.Id,
                        UsageType = usageType,
                        Timestamp = timestamp,
                        IsRoaming = subscriber.IsRoaming,
                        IsPeak = isPeak
                    };

                    switch (usageType)
                    {
                        case UsageType.Call:
                            record.CallMinutes = random.Next(1, 10);
                            break;

                        case UsageType.Data:
                            record.DataMB = random.Next(50, 500);
                            break;

                        case UsageType.SMS:
                            record.SMSCount = random.Next(1, 5);
                            break;
                    }

                    usageRecords.Add(record);
                }
            }

            await context.UsageRecords.AddRangeAsync(usageRecords);
            await context.SaveChangesAsync();
        }
    }
}