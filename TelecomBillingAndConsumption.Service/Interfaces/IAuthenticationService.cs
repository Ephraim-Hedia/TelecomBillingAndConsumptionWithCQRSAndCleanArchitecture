using TelecomBillingAndConsumption.Data.Entities.Identity;

namespace TelecomBillingAndConsumption.Service.Interfaces
{
    public interface IAuthenticationService
    {
        public Task<string> GetJwtToken(User user);
    }
}
