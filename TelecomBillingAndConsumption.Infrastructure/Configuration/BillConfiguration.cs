using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TelecomBillingAndConsumption.Data.Entities;

namespace TelecomBillingAndConsumption.Infrastructure.Configuration
{
    public class BillConfiguration : IEntityTypeConfiguration<Bill>
    {
        public void Configure(EntityTypeBuilder<Bill> builder)
        {
            builder.HasKey(b => b.Id);

            builder.Property(b => b.Month)
                .IsRequired()
                .HasMaxLength(7); // Example: "YYYY-MM"

            builder.Property(b => b.PlanFee).HasColumnType("decimal(18,2)");
            builder.Property(b => b.UsageCost).HasColumnType("decimal(18,2)");
            builder.Property(b => b.RoamingSurcharge).HasColumnType("decimal(18,2)");
            builder.Property(b => b.ExtraUsageCost).HasColumnType("decimal(18,2)");
            builder.Property(b => b.LoyaltyDiscount).HasColumnType("decimal(18,2)");
            builder.Property(b => b.VatAmount).HasColumnType("decimal(18,2)");
            builder.Property(b => b.TotalAmount).HasColumnType("decimal(18,2)");

            builder.HasOne(b => b.Subscriber)
                .WithMany(s => s.Bills)
                .HasForeignKey(b => b.SubscriberId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(b => b.BillDetail)
                .WithOne(d => d.Bill)
                .HasForeignKey<BillDetail>(d => d.BillId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(b => b.Payment)
                .WithOne(p => p.Bill)
                .HasForeignKey<Payment>(p => p.BillId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Property(b => b.RowVersion)
                .IsRowVersion();

            builder.HasQueryFilter(b => !b.IsDeleted);
        }
    }
}
