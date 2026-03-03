namespace TelecomBillingAndConsumption.Data.Entities
{
    public class BillDetail : BaseEntity
    {
        public int BillId { get; set; }

        public int UsageRecordId { get; set; }

        public decimal CallCost { get; set; }

        public decimal DataCost { get; set; }

        public decimal SmsCost { get; set; }

        public decimal ExtraCharge { get; set; }

        public decimal RoamingCharge { get; set; }

        // Navigation
        public Bill Bill { get; set; } = null!;

        public UsageRecord UsageRecord { get; set; } = null!;
    }
}
