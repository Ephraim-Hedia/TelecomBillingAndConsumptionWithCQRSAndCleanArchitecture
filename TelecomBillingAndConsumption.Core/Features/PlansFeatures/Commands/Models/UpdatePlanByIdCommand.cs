using MediatR;
using TelecomBillingAndConsumption.Core.Bases;

namespace TelecomBillingAndConsumption.Core.Features.PlansFeatures.Commands.Models
{
    public class UpdatePlanByIdCommand : IRequest<Response<string>> // "Plan Updated Successfully"
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public decimal MonthlyFee { get; set; }

        public int IncludedCallMinutes { get; set; }

        public decimal IncludedDataMB { get; set; }

        public int IncludedSMS { get; set; }

        public bool IsActive { get; set; }
    }
}
