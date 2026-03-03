namespace TelecomBillingAndConsumption.Core.Features.SubscribersFeatures.Commands.Models
{
    public class AddSubscriberCommand
    {
        public string Name { get; set; }

        public string PhoneNumber { get; set; }

        public string Country { get; set; }

        public int PlanId { get; set; }

        public bool IsRoaming { get; set; }
    }
}
