using FluentValidation;
using TelecomBillingAndConsumption.Core.Features.BillingFeatures.Queries.Models;

namespace TelecomBillingAndConsumption.Core.Features.BillingFeatures.Queries.Validators
{
    public class GetBillingDetailsByBillIdValidator
        : AbstractValidator<GetBillingDetailsByBillIdQuery>
    {
        public GetBillingDetailsByBillIdValidator()
        {
            RuleFor(x => x.BillId)
                .GreaterThan(0)
                .WithMessage("BillId must be greater than 0.");
        }
    }
}
