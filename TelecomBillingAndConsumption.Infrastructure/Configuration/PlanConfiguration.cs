using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TelecomBillingAndConsumption.Data.Entities;

namespace TelecomBillingAndConsumption.Infrastructure.Configuration
{
    public class PlanConfiguration : IEntityTypeConfiguration<Plan>
    {
        public void Configure(EntityTypeBuilder<Plan> builder)
        {
            builder.ToTable("Plans");

            builder.HasKey(p => p.Id);

            builder.Property(p => p.Name)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(p => p.MonthlyFee)
                .HasPrecision(18, 2);

            builder.Property(p => p.IncludedDataMB)
                .HasPrecision(18, 2);

            builder.Property(p => p.RowVersion)
                .IsRowVersion();

            builder.HasQueryFilter(p => !p.IsDeleted);
        }
    }
}
