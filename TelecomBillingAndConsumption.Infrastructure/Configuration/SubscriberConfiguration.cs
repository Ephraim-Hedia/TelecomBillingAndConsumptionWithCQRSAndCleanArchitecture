using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TelecomBillingAndConsumption.Data.Entities;

namespace TelecomBillingAndConsumption.Infrastructure.Configuration
{
    public class SubscriberConfiguration : IEntityTypeConfiguration<Subscriber>
    {
        public void Configure(EntityTypeBuilder<Subscriber> builder)
        {
            builder.ToTable("Subscribers");

            builder.HasKey(s => s.Id);

            builder.Property(s => s.Name)
                .IsRequired()
                .HasMaxLength(150);

            builder.Property(s => s.PhoneNumber)
                .IsRequired()
                .HasMaxLength(20);

            builder.HasIndex(s => s.PhoneNumber)
                .IsUnique();

            builder.HasIndex(s => s.UserId);

            builder.HasOne(s => s.Plan)
                .WithMany(p => p.Subscribers)
                .HasForeignKey(s => s.PlanId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Property(s => s.RowVersion)
                .IsRowVersion();

            builder.HasQueryFilter(s => !s.IsDeleted);
        }
    }
}
