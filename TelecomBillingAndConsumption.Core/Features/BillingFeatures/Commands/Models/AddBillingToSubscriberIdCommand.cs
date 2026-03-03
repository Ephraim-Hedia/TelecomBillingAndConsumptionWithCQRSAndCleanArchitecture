using MediatR;
using TelecomBillingAndConsumption.Core.Bases;

namespace TelecomBillingAndConsumption.Core.Features.BillingFeatures.Commands.Models
{
    public class AddBillingToSubscriberIdCommand : IRequest<Response<int>>
    {

    }
}
