using TelecomBillingAndConsumption.Data.Entities.Identity;

namespace TelecomBillingAndConsumption.Data.Entities
{
    public class Subscriber : BaseEntity
    {
        public string Name { get; set; } = string.Empty;

        public string PhoneNumber { get; set; } = string.Empty;

        public string Country { get; set; } = string.Empty;

        public bool IsRoaming { get; set; }

        public bool IsActive { get; set; } = true;

        public DateTime SubscriptionStartDate { get; set; }

        // Foreign Keys
        public int PlanId { get; set; }

        public int UserId { get; set; }

        public User User { get; set; }

        // Navigation
        public Plan Plan { get; set; } = null!;

        public ICollection<UsageRecord>? UsageRecords { get; set; } = new HashSet<UsageRecord>();

        public ICollection<Bill>? Bills { get; set; } = new HashSet<Bill>();
    }
}
