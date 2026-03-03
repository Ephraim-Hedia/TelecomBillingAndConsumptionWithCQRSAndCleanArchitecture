namespace TelecomBillingAndConsumption.Core.Features.PlansFeatures.Queries.Results
{
    public class GetPlanByIdResponse
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
