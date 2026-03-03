namespace TelecomBillingAndConsumption.Core.Features.SubscribersFeatures.Queries.Results
{
    public class GetSubscriberByPhoneNumberResponse
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string PhoneNumber { get; set; }

        public string Country { get; set; }

        public bool IsRoaming { get; set; }

        public bool IsActive { get; set; }

        public int PlanId { get; set; }

        public string PlanName { get; set; }

        public DateTime SubscriptionStartDate { get; set; }
    }
}
