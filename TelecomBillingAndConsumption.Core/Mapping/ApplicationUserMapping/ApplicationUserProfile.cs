using AutoMapper;

namespace TelecomBillingAndConsumption.Core.Mapping.ApplicationUserMapping
{
    public partial class ApplicationUserProfile : Profile
    {
        public ApplicationUserProfile()
        {
            AddUserMapping();
            GetUserPaginatedMapping();
            GetUserByIdMapping();
            UpdateUserMapping();
        }
    }
}
