using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Localization;
using TelecomBillingAndConsumption.Core.Bases;
using TelecomBillingAndConsumption.Core.Features.ApplicationUser.Queries.Models;
using TelecomBillingAndConsumption.Core.Features.ApplicationUser.Queries.Results;
using TelecomBillingAndConsumption.Core.Resources;
using TelecomBillingAndConsumption.Core.Wrappers;
using TelecomBillingAndConsumption.Data.Entities.Identity;

namespace TelecomBillingAndConsumption.Core.Features.ApplicationUser.Queries.Handlers
{
    public class UserQueryHandler : ResponseHandler
            , IRequestHandler<GetUserPaginatedQuery, PaginatedResult<GetUserPaginatedResponse>>
            , IRequestHandler<GetUserByIdQuery, Response<GetUserByIdResponse>>
    {
        #region Fields
        private readonly IStringLocalizer<SharedResources> _localizer;
        private readonly UserManager<User> _userManager;
        private readonly IMapper _mapper;
        #endregion



        #region Constructors
        public UserQueryHandler(
            UserManager<User> userManager,
            IMapper mapper,
            IStringLocalizer<SharedResources> stringLocalizer
            ) : base(stringLocalizer)
        {
            _userManager = userManager;
            _mapper = mapper;
            _localizer = stringLocalizer;
        }
        #endregion


        #region Handle Functions
        public async Task<PaginatedResult<GetUserPaginatedResponse>> Handle(GetUserPaginatedQuery request, CancellationToken cancellationToken)
        {
            var applicationUsers = _userManager.Users.AsQueryable();
            var paginatedList = await _mapper.ProjectTo<GetUserPaginatedResponse>(applicationUsers)
                .ToPaginatedListAsync(request.PageNumber, request.PageSize);
            return paginatedList;
        }

        public async Task<Response<GetUserByIdResponse>> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
        {
            var applicationUser = await _userManager.FindByIdAsync(request.Id.ToString());
            if (applicationUser == null)
                return NotFound<GetUserByIdResponse>(_localizer[SharedResourcesKeys.NotFound]);

            var mappedUser = _mapper.Map<GetUserByIdResponse>(applicationUser);
            return Success(mappedUser);

        }
        #endregion
    }
}
