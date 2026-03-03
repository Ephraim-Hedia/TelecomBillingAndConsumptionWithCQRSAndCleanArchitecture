namespace TelecomBillingAndConsumption.Core.Features.PlansFeatures.Queries.Results
{
    public class GetAllPlansPaginatedResponse
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public decimal MonthlyFee { get; set; }

        public int IncludedCallMinutes { get; set; }

        public decimal IncludedDataMB { get; set; }

        public int IncludedSMS { get; set; }

        public bool IsActive { get; set; }
    }
}
