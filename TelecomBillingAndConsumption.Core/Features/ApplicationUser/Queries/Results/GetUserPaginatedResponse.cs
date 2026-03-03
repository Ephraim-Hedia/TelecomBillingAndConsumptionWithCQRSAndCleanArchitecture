namespace TelecomBillingAndConsumption.Core.Features.ApplicationUser.Queries.Results
{
    public class GetUserPaginatedResponse
    {
        public string FullName { get; set; }
        public string Email { get; set; }
        public string? Address { get; set; }
        public string? Country { get; set; }
    }
}
