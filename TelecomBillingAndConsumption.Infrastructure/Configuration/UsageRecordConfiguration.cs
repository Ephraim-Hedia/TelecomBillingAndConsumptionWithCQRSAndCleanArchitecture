using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TelecomBillingAndConsumption.Data.Entities;

namespace TelecomBillingAndConsumption.Infrastructure.Configuration
{
    public class UsageRecordConfiguration : IEntityTypeConfiguration<UsageRecord>
    {
        public void Configure(EntityTypeBuilder<UsageRecord> builder)
        {
            builder.ToTable("UsageRecords");

            builder.HasKey(u => u.Id);

            builder.Property(u => u.UsageType)
                .HasConversion<int>();

            builder.Property(u => u.UnitPrice)
                .HasPrecision(18, 4);

            builder.Property(u => u.TotalCost)
                .HasPrecision(18, 4);

            builder.Property(u => u.DataMB)
                .HasPrecision(18, 2);

            builder.HasIndex(u => u.SubscriberId);
            builder.HasIndex(u => u.Timestamp);
            builder.HasIndex(u => new { u.SubscriberId, u.Timestamp });

            builder.HasOne(u => u.Subscriber)
                .WithMany(s => s.UsageRecords)
                .HasForeignKey(u => u.SubscriberId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Property(u => u.RowVersion)
                .IsRowVersion();

            builder.HasQueryFilter(u => !u.IsDeleted);
        }
    }
}
