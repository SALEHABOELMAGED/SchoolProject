using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Localization;
using SchoolProject.Core.Bases;
using SchoolProject.Core.Features.ApplicationUser.Queries.Models;
using SchoolProject.Core.Features.ApplicationUser.Queries.Results;
using SchoolProject.Core.Resources;
using SchoolProject.Core.Wrappers;
using SchoolProject.Data.Entities.Identity;

namespace SchoolProject.Core.Features.ApplicationUser.Queries.Handlers
{
    public class UserQueryHandler : ResponseHandler,
        IRequestHandler<GetUserPaginationQuery, Response<PaginatedResult<GetUserPaginationQueryResult>>>,
        IRequestHandler<GetUserByIdQuery, Response<GetUserByIdQueryResult>>
    {
        #region Fields
        public readonly IStringLocalizer<SharedResources> _localizer;
        private readonly UserManager<AppUser> _userManager;
        private readonly IMapper _mapper;
        #endregion

        #region Constructor
        public UserQueryHandler(UserManager<AppUser> userManager,
                                IMapper mapper,
                                IStringLocalizer<SharedResources> localizer) : base(localizer)
        {
            _userManager = userManager;
            _mapper = mapper;
        }
        #endregion

        #region Handle Functions
        public async Task<Response<PaginatedResult<GetUserPaginationQueryResult>>> Handle(GetUserPaginationQuery request, CancellationToken cancellationToken)
        {
            var query = _userManager.Users
                .Select(u => new GetUserPaginationQueryResult
                {
                    FullName = u.DisplayName,
                    Address = u.Address,
                    Email = u.Email,
                    Country = u.Country
                });

            var paginatedResult = await query.ToPaginatedResultAsync(request.PageNumber, request.PageSize);
            return Success(paginatedResult);
        }
        public async Task<Response<GetUserByIdQueryResult>> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByIdAsync(request.Id.ToString());
            if (user == null)
            {
                return NotFound<GetUserByIdQueryResult>("User not found");
            }

            var result = new GetUserByIdQueryResult
            {
                FullName = user.DisplayName,
                Address = user.Address,
                Email = user.Email,
                Country = user.Country
            };

            return Success(result);
        }
        #endregion
    }

}