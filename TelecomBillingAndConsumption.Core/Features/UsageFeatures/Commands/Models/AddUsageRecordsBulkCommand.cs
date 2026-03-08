using MediatR;
using TelecomBillingAndConsumption.Core.Bases;

namespace TelecomBillingAndConsumption.Core.Features.UsageFeatures.Commands.Models
{
    public class AddUsageRecordsBulkCommand : IRequest<Response<string>>
    {
        public List<AddUsageRecordCommand> Records { get; set; } = new();
    }
}
