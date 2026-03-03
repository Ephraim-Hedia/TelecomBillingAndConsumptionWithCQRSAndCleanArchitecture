namespace TelecomBillingAndConsumption.Core.Features.BillingFeatures.Queries.Results
{
    public class GetBillingDetailsByBillIdResponse
    {
        public int UsageRecordId { get; set; }

        public decimal CallCost { get; set; }

        public decimal DataCost { get; set; }

        public decimal SmsCost { get; set; }

        public decimal RoamingCharge { get; set; }

        public decimal ExtraCharge { get; set; }
    }
}
