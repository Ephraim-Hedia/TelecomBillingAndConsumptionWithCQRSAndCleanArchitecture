using FluentValidation;
using TelecomBillingAndConsumption.Core.Features.DashboardFeatures.Queries.Models;

namespace TelecomBillingAndConsumption.Core.Features.DashboardFeatures.Queries.Validators
{
    public class GetDashboardTopCustomersValidator
        : AbstractValidator<GetDashboardTopCustomersQuery>
    {
        public GetDashboardTopCustomersValidator()
        {
            RuleFor(x => x.TopN)
                .GreaterThan(0)
                .WithMessage("TopN must be greater than 0.")
                .LessThanOrEqualTo(100)
                .WithMessage("TopN cannot exceed 100.");
        }
    }
}
