using FluentValidation;
using TelecomBillingAndConsumption.Core.Features.DashboardFeatures.Queries.Models;

namespace TelecomBillingAndConsumption.Core.Features.DashboardFeatures.Queries.Validators
{
    public class GetUsageStatisticsQueryValidator : AbstractValidator<GetUsageStatisticsQuery>
    {
        public GetUsageStatisticsQueryValidator()
        {
            RuleFor(q => q.Month).InclusiveBetween(1, 12);
            RuleFor(q => q.Year).GreaterThan(2000);
        }
    }
}
