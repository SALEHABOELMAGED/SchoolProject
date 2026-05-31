using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Localization;
using SchoolProject.Core.Bases;
using SchoolProject.Core.Features.Authentication.Commands.Models;
using SchoolProject.Data.Entities.Identity;
using SchoolProject.Data.Helpers;
using SchoolProject.Service.Abstracts;

namespace SchoolProject.Core.Features.Authentication.Commands.Handlers
{
    public class AuthenticationCommandHandler : ResponseHandler,
        IRequestHandler<SignInCommand, Response<JwtAuthenticationResult>>,
        IRequestHandler<RefreshTokenCommand, Response<JwtAuthenticationResult>>
    {
        #region Fields
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly IAuthenticationService _authenticationService;
        private readonly IStringLocalizer<SchoolProject.Core.Resources.SharedResources> _stringLocalizer;
        #endregion

        #region Constructor
        public AuthenticationCommandHandler(IStringLocalizer<SchoolProject.Core.Resources.SharedResources> stringLocalizer,
                                            UserManager<AppUser> userManager, SignInManager<AppUser> signInManager,
                                            IAuthenticationService authenticationService
                                            ) : base(stringLocalizer)
        {
            _stringLocalizer = stringLocalizer;
            _userManager = userManager;
            _signInManager = signInManager;
            _authenticationService = authenticationService;
        }
        #endregion
        #region Actions
        public async Task<Response<JwtAuthenticationResult>> Handle(SignInCommand request, CancellationToken cancellationToken)
        {
            // Check if user exists
            var user = await _userManager.FindByNameAsync(request.Username);
            if (user == null)
                return BadRequest<JwtAuthenticationResult>(_stringLocalizer["UserNotFound"]);

            // Validate password
            var signIn = await _signInManager.CheckPasswordSignInAsync(user, request.Password, false);
            if (!signIn.Succeeded)
                return BadRequest<JwtAuthenticationResult>(_stringLocalizer["InvalidCredentials"]);

            // Generate and return JWT token
            var token = await _authenticationService.GetJWTToken(user);
            return Success<JwtAuthenticationResult>(token);
        }

        public async Task<Response<JwtAuthenticationResult>> Handle(RefreshTokenCommand request, CancellationToken cancellationToken)
        {
            // Validate the refresh token and generate a new JWT token
            var result = await _authenticationService.GetRefreshToken(request.AccessToken, request.RefreshToken);
            if (result == null)
                return BadRequest<JwtAuthenticationResult>(_stringLocalizer["InvalidRefreshToken"]);
            return Success(result);

        }
        #endregion
    }
}
