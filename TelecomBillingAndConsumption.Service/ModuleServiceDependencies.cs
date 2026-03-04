using Microsoft.Extensions.DependencyInjection;
using TelecomBillingAndConsumption.Service.Implementation;
using TelecomBillingAndConsumption.Service.Implementation.PlanService;
using TelecomBillingAndConsumption.Service.Interfaces;
using TelecomBillingAndConsumption.Service.Interfaces.PlanService;

namespace TelecomBillingAndConsumption.Service
{
    public static class ModuleServiceDependencies
    {
        public static IServiceCollection AddServiceDependencies(this IServiceCollection services)
        {
            // Plan Service
            services.AddScoped<IPlanService, PlanService>();
            services.AddScoped<IAuthenticationService, AuthenticationService>();
            return services;
        }

    }
}
