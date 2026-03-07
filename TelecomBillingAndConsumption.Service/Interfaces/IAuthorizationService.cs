namespace TelecomBillingAndConsumption.Service.Interfaces
{
    public interface IAuthorizationService
    {
        public Task<string> AddRoleAsync(string roleName);
        public Task<bool> IsRoleExistByName(string roleName);
    }
}
