using Microsoft.EntityFrameworkCore;
using TelecomBillingAndConsumption.Data.Entities;
using TelecomBillingAndConsumption.Infrastructure.InfrastructureBases;
using TelecomBillingAndConsumption.Service.Interfaces.PlanService;

namespace TelecomBillingAndConsumption.Service.Implementation.PlanService
{
    /// <summary>
    /// Plan Service Implementation
    /// Contains business logic for Plan operations
    /// </summary>
    public class PlanService : IPlanService
    {
        #region Fields
        private readonly IGenericRepository<Plan> _planRepository;
        #endregion

        #region Constructors
        public PlanService(IGenericRepository<Plan> planRepository)
        {
            _planRepository = planRepository;
        }
        #endregion

        #region Handle Functions
        public async Task<List<Plan>> GetAllAsync()
        {
            return await _planRepository.GetTableNoTracking()
                .Where(p => !p.IsDeleted)
                .ToListAsync();
        }

        public async Task<Plan?> GetByIdAsync(int id)
        {
            var plan = await _planRepository.GetByIdAsync(id);
            return (plan == null || plan.IsDeleted) ? null : plan;
        }

        public async Task<int> AddAsync(Plan plan)
        {
            var result = await _planRepository.AddAsync(plan);
            return result.Id;
        }

        public async Task<bool> UpdateAsync(Plan plan)
        {
            await _planRepository.UpdateAsync(plan);
            return true;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var plan = await _planRepository.GetByIdAsync(id);
            if (plan == null) return false;
            plan.IsDeleted = true;
            await _planRepository.UpdateAsync(plan);
            return true;
        }
        public IQueryable<Plan> GetPlansAsQuarable()
        {
            return _planRepository.GetTableNoTracking()
                .Where(p => !p.IsDeleted && p.IsActive);
        }

        public async Task<bool> ActivateAsync(int id)
        {
            var plan = await _planRepository.GetByIdAsync(id);
            if (plan == null || plan.IsDeleted) return false;
            plan.IsActive = true;
            plan.UpdatedAt = DateTime.UtcNow;
            await _planRepository.UpdateAsync(plan);
            return true;
        }

        public async Task<bool> DeactivateAsync(int id)
        {
            var plan = await _planRepository.GetByIdAsync(id);
            if (plan == null || plan.IsDeleted) return false;
            plan.IsActive = false;
            plan.UpdatedAt = DateTime.UtcNow;
            await _planRepository.UpdateAsync(plan);
            return true;
        }
        #endregion
    }
}