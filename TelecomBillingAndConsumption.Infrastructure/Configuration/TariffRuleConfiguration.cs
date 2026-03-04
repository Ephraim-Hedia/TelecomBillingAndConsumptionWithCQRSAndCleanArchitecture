using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TelecomBillingAndConsumption.Data.Entities;

namespace TelecomBillingAndConsumption.Infrastructure.Configuration
{
    public class TariffRuleConfiguration : IEntityTypeConfiguration<TariffRule>
    {
        public void Configure(EntityTypeBuilder<TariffRule> builder)
        {
            builder.ToTable("TariffRules");

            builder.HasKey(t => t.Id);

            builder.Property(t => t.UsageType)
                .HasConversion<int>();

            builder.Property(t => t.PricePerUnit)
                .HasPrecision(18, 4);

            builder.HasIndex(t => new
            {
                t.UsageType,
                t.IsRoaming,
                t.IsPeak,
            });

            builder.HasQueryFilter(t => !t.IsDeleted);
        }
    }
}
