using MediatR;
using TelecomBillingAndConsumption.Core.Bases;

namespace TelecomBillingAndConsumption.Core.Features.UsageFeatures.Commands.Models
{
    public class DeleteUsageRecordByIdCommand : IRequest<Response<string>>
    {
        public int Id { get; set; }
    }
}
