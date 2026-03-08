using FluentValidation;
using TelecomBillingAndConsumption.Core.Features.DashboardFeatures.Queries.Models;

namespace TelecomBillingAndConsumption.Core.Features.DashboardFeatures.Queries.Validators
{
    public class GetDashboardRevenueValidator
        : AbstractValidator<GetDashboardRevenueQuery>
    {
        public GetDashboardRevenueValidator()
        {
            RuleFor(x => x.Month)
                .InclusiveBetween(1, 12)
                .WithMessage("Month must be between 1 and 12.");

            RuleFor(x => x.Year)
                .GreaterThanOrEqualTo(2000)
                .WithMessage("Year must be greater than or equal to 2000.")
                .LessThanOrEqualTo(DateTime.UtcNow.Year)
                .WithMessage("Year cannot be in the future.");


        }
    }
}
