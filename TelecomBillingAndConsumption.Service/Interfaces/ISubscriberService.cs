using TelecomBillingAndConsumption.Data.Entities;

namespace TelecomBillingAndConsumption.Service.Interfaces
{
    public interface ISubscriberService
    {
        IQueryable<Subscriber> QuerySubscribers();
        Task<Subscriber?> GetByIdAsync(int id);
        Task<int> AddAsync(Subscriber s);
        Task<bool> UpdateAsync(Subscriber s);
        Task<bool> DeleteAsync(int id);
        Task<bool> ActivateAsync(int id);
        Task<bool> DeactivateAsync(int id);
        public Task<bool> ExistsByPhoneAsync(string phoneNumber);
    }
}
