using Microsoft.EntityFrameworkCore;
using TelecomBillingAndConsumption.Data.Entities;
using TelecomBillingAndConsumption.Infrastructure.Interfaces;
using TelecomBillingAndConsumption.Service.Interfaces;

namespace TelecomBillingAndConsumption.Service.Implementation
{
    public class SubscriberService : ISubscriberService
    {

        #region Fields
        private readonly ISubscriberRepository _subscriberRepository;
        #endregion

        #region Constructors
        public SubscriberService(ISubscriberRepository subscriberRepository)
        {
            _subscriberRepository = subscriberRepository;
        }
        #endregion

        #region Handle Functions
        public IQueryable<Subscriber> QuerySubscribers()
        {
            return _subscriberRepository.QueryWithIncludes();
        }

        public async Task<Subscriber?> GetByIdAsync(int id)
        {
            var s = await _subscriberRepository.GetSubscriberByIdWithIncludes(id);
            return s != null && !s.IsDeleted ? s : null;
        }

        public async Task<int> AddAsync(Subscriber s)
        {
            s.SubscriptionStartDate = DateTime.UtcNow;
            var result = await _subscriberRepository.AddAsync(s);
            return result.Id;
        }

        public async Task<bool> UpdateAsync(Subscriber s)
        {
            await _subscriberRepository.UpdateAsync(s);
            return true;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var s = await _subscriberRepository.GetByIdAsync(id);
            if (s == null || s.IsDeleted)
                return false;
            s.IsDeleted = true;
            await _subscriberRepository.UpdateAsync(s);
            return true;
        }

        public async Task<bool> ActivateAsync(int id)
        {
            var s = await _subscriberRepository.GetByIdAsync(id);
            if (s == null || s.IsDeleted)
                return false;
            s.IsActive = true;
            s.UpdatedAt = DateTime.UtcNow;
            await _subscriberRepository.UpdateAsync(s);
            return true;
        }

        public async Task<bool> DeactivateAsync(int id)
        {
            var s = await _subscriberRepository.GetByIdAsync(id);
            if (s == null || s.IsDeleted)
                return false;
            s.IsActive = false;
            s.UpdatedAt = DateTime.UtcNow;
            await _subscriberRepository.UpdateAsync(s);
            return true;
        }

        public async Task<bool> ExistsByPhoneAsync(string phoneNumber)
        {
            return await _subscriberRepository.GetTableNoTracking().AnyAsync(s => s.PhoneNumber == phoneNumber && !s.IsDeleted);
        }


        public async Task<bool> UpdateSubscriberPlanAsync(int subscriberId, int newPlanId)
        {
            return await _subscriberRepository.UpdateSubscriberPlanAsync(subscriberId, newPlanId);
        }
        #endregion

    }
}
