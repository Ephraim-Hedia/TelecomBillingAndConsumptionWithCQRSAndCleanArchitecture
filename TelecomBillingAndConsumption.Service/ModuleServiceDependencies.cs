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
            // Tariff Service
            services.AddScoped<ITariffService, TariffService>();
            // Subscriber Service
            services.AddScoped<ISubscriberService, SubscriberService>();
            // Usage Records Service
            services.AddScoped<IUsageRecordService, UsageRecordService>();
            // Plan Limit Service 
            services.AddScoped<IPlanLimitService, PlanLimitService>();

            // Usage Summary Service
            services.AddScoped<IUsageSummaryService, UsageSummaryService>();

            // Bill Service
            services.AddScoped<IBillService, BillService>();

            // Dashboard Service 
            services.AddScoped<IDashboardService, DashboardService>();

            // To Improve the speed of getting Tariff
            services.AddSingleton<ITariffCacheService, TariffCacheService>();

            services.AddScoped<IAuthenticationService, AuthenticationService>();

            services.AddScoped<IAuthorizationService, AuthorizationService>();
            return services;
        }

    }
}
