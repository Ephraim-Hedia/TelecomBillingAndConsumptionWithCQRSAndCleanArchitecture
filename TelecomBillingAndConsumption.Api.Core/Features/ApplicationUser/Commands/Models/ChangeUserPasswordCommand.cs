using MediatR;
using TelecomBillingAndConsumption.Core.Bases;

namespace TelecomBillingAndConsumption.Core.Features.ApplicationUser.Commands.Models
{
    public class ChangeUserPasswordCommand : IRequest<Response<string>>
    {
        public int UserId { get; set; }
        public string CurrentPassword { get; set; }
        public string NewPassword { get; set; }
        public string ConfirmNewPassword { get; set; }
    }
}
