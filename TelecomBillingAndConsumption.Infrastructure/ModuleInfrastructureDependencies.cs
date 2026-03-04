using Microsoft.Extensions.DependencyInjection;
using TelecomBillingAndConsumption.Infrastructure.InfrastructureBases;
using TelecomBillingAndConsumption.Infrastructure.Interfaces;
using TelecomBillingAndConsumption.Infrastructure.Repositories;

namespace TelecomBillingAndConsumption.Infrastructure
{
    public static class ModuleInfrastructureDependencies
    {
        public static IServiceCollection AddInfrastructureDependencies(this IServiceCollection services)
        {
            services.AddTransient(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            services.AddScoped<IPlanRepository, PlanRepository>();

            return services;
        }
    }
}
