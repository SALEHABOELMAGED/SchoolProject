using MediatR;
using Microsoft.Extensions.Localization;
using SchoolProject.Core.Bases;
using SchoolProject.Core.Features.Authentication.Queries.Models;
using SchoolProject.Service.Abstracts;

namespace SchoolProject.Core.Features.Authentication.Queries.Handlers
{
    public class AuthenticationQueryHandler : ResponseHandler,
        IRequestHandler<AuthorizeUserQuery, Response<string>>
    {
        #region Fields
        private readonly IAuthenticationService _authenticationService;
        private readonly IStringLocalizer<SchoolProject.Core.Resources.SharedResources> _stringLocalizer;
        #endregion

        #region Constructor
        public AuthenticationQueryHandler(IStringLocalizer<SchoolProject.Core.Resources.SharedResources> stringLocalizer,
                                            IAuthenticationService authenticationService
                                            ) : base(stringLocalizer)
        {
            _stringLocalizer = stringLocalizer;
            _authenticationService = authenticationService;
        }
        #endregion

        #region Actions
        public async Task<Response<string>> Handle(AuthorizeUserQuery request, CancellationToken cancellationToken)
        {
            var result = await _authenticationService.ValidateToken(request.AccessToken);
            if (result == "Not Expired")
                return Success(result);
            return BadRequest<string>("Expired Token");
        }
        #endregion
    }

}

