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

            builder.HasKey(bd => bd.Id);

            builder.Property(bd => bd.CallCost).HasPrecision(18, 2);
            builder.Property(bd => bd.DataCost).HasPrecision(18, 2);
            builder.Property(bd => bd.SmsCost).HasPrecision(18, 2);
            builder.Property(bd => bd.ExtraCharge).HasPrecision(18, 2);
            builder.Property(bd => bd.RoamingCharge).HasPrecision(18, 2);

            builder.HasOne(bd => bd.Bill)
                .WithMany(b => b.BillDetails)
                .HasForeignKey(bd => bd.BillId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(bd => bd.UsageRecord)
                .WithMany(u => u.BillDetails)
                .HasForeignKey(bd => bd.UsageRecordId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasQueryFilter(bd => !bd.IsDeleted);
        }
    }
}
