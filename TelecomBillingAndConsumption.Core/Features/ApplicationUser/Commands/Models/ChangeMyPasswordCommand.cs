using MediatR;
using TelecomBillingAndConsumption.Core.Bases;

namespace TelecomBillingAndConsumption.Core.Features.ApplicationUser.Commands.Models
{
    public class ChangeMyPasswordCommand : IRequest<Response<string>>
    {
        public string CurrentPassword { get; set; }
        public string NewPassword { get; set; }

        // This will be filled automatically from the token
        public int UserId { get; set; }
    }
}
