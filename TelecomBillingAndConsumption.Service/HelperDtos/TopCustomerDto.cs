namespace TelecomBillingAndConsumption.Service.HelperDtos
{
    public class TopCustomerDto
    {
        public int SubscriberId { get; set; }
        public string? Name { get; set; }
        public string? PhoneNumber { get; set; }
        public decimal TotalCost { get; set; }
    }
}
