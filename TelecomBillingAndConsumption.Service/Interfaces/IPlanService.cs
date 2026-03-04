using TelecomBillingAndConsumption.Data.Entities;

namespace TelecomBillingAndConsumption.Service.Interfaces.PlanService
{
    /// <summary>
    /// Plan Service Interface
    /// Defines business operations related to Plans
    /// </summary>
    public interface IPlanService
    {
        Task<List<Plan>> GetAllAsync();
        Task<Plan?> GetByIdAsync(int id);
        Task<int> AddAsync(Plan plan);
        Task<bool> UpdateAsync(Plan plan);
        Task<bool> DeleteAsync(int id);
        public IQueryable<Plan> GetPlansAsQuarable();
        Task<bool> ActivateAsync(int id);
        Task<bool> DeactivateAsync(int id);
    }
}