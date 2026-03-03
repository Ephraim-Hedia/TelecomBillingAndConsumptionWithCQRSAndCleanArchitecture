using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TelecomBillingAndConsumption.Data.Entities;

namespace TelecomBillingAndConsumption.Infrastructure.Configuration
{
    public class BillConfiguration : IEntityTypeConfiguration<Bill>
    {
        public void Configure(EntityTypeBuilder<Bill> builder)
        {
            builder.ToTable("Bills");

            builder.HasKey(b => b.Id);

            builder.HasIndex(b => new { b.SubscriberId, b.BillingMonth, b.BillingYear })
                .IsUnique();

            builder.Property(b => b.PlanFee).HasPrecision(18, 2);
            builder.Property(b => b.UsageCost).HasPrecision(18, 2);
            builder.Property(b => b.RoamingSurcharge).HasPrecision(18, 2);
            builder.Property(b => b.ExtraUsageCost).HasPrecision(18, 2);
            builder.Property(b => b.VatAmount).HasPrecision(18, 2);
            builder.Property(b => b.LoyaltyDiscount).HasPrecision(18, 2);
            builder.Property(b => b.TotalAmount).HasPrecision(18, 2);

            builder.HasOne(b => b.Subscriber)
                .WithMany(s => s.Bills)
                .HasForeignKey(b => b.SubscriberId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Property(b => b.RowVersion)
                .IsRowVersion();

            builder.HasQueryFilter(b => !b.IsDeleted);
        }
    }
}
