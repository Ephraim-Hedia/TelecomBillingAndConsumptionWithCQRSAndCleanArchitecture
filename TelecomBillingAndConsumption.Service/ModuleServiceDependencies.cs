using Microsoft.Extensions.DependencyInjection;
using TelecomBillingAndConsumption.Service.Implementation;
using TelecomBillingAndConsumption.Service.Interfaces;

namespace TelecomBillingAndConsumption.Service
{
    public static class ModuleServiceDependencies
    {
        public static IServiceCollection AddServiceDependencies(this IServiceCollection services)
        {

            services.AddScoped<IAuthenticationService, AuthenticationService>();
            return services;
        }

    }
}
