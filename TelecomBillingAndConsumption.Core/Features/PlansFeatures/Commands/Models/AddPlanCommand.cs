using MediatR;
using TelecomBillingAndConsumption.Core.Bases;

namespace TelecomBillingAndConsumption.Core.Features.PlansFeatures.Commands.Models
{
    public class AddPlanCommand : IRequest<Response<int>>
    {
        public string Name { get; set; }

        public decimal MonthlyFee { get; set; }

        public int IncludedCallMinutes { get; set; }

        public decimal IncludedDataMB { get; set; }

        public int IncludedSMS { get; set; }
    }
}
