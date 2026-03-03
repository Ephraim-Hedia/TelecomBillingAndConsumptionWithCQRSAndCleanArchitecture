using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using TelecomBillingAndConsumption.Core.Behaviors;

namespace TelecomBillingAndConsumption.Core
{
    public static class ModuleCoreDependencies
    {
        public static IServiceCollection AddCoreDependencies(this IServiceCollection services)
        {
            // configuration for MediatR
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(ModuleCoreDependencies).Assembly));

            // Configuration for AutoMapper
            services.AddAutoMapper(cfg => cfg.AddMaps(typeof(ModuleCoreDependencies).Assembly));

            // Get Validators
            services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
            return services;
        }

    }
}
