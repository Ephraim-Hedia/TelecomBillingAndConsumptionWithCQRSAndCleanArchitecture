using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TelecomBillingAndConsumption.Data.Entities;

namespace TelecomBillingAndConsumption.Infrastructure.Configuration
{
    public class BillDetailConfiguration : IEntityTypeConfiguration<BillDetail>
    {
        public void Configure(EntityTypeBuilder<BillDetail> builder)
        {
            builder.ToTable("BillDetails");

            builder.HasKey(d => d.Id);

            builder.Property(d => d.PeakCalls).IsRequired();
            builder.Property(d => d.OffPeakCalls).IsRequired();
            builder.Property(d => d.DataMB).HasColumnType("decimal(18,2)").IsRequired();
            builder.Property(d => d.Sms).IsRequired();

            builder.HasOne(d => d.Bill)
                .WithOne(b => b.BillDetail)
                .HasForeignKey<BillDetail>(d => d.BillId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(d => d.UsageRecord)
                .WithMany() // If UsageRecord does NOT track BillDetail directly.
                .HasForeignKey("UsageRecordId")
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasQueryFilter(bd => !bd.IsDeleted);
        }
    }
}
