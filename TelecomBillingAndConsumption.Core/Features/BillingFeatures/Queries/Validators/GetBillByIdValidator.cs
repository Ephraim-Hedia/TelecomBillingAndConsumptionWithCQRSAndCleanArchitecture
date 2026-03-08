using FluentValidation;
using TelecomBillingAndConsumption.Core.Features.BillingFeatures.Queries.Models;

namespace TelecomBillingAndConsumption.Core.Features.BillingFeatures.Queries.Validators
{
    public class GetBillByIdValidator : AbstractValidator<GetBillByIdQuery>
    {
        public GetBillByIdValidator()
        {
            RuleFor(x => x.BillId)
                .GreaterThan(0)
                .WithMessage("BillId must be greater than 0.");
        }
    }
}
