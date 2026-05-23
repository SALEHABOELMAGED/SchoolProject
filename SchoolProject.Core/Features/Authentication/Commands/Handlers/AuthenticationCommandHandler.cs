using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Localization;
using SchoolProject.Core.Bases;
using SchoolProject.Core.Features.Authentication.Commands.Models;
using SchoolProject.Data.Entities.Identity;
using SchoolProject.Service.Abstracts;

namespace SchoolProject.Core.Features.Authentication.Commands.Handlers
{
    public class AuthenticationCommandHandler : ResponseHandler,
        IRequestHandler<SignInCommand, Response<string>>
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
        public async Task<Response<string>> Handle(SignInCommand request, CancellationToken cancellationToken)
        {
            // Check if user exists
            var user = await _userManager.FindByNameAsync(request.Username);
            if (user == null)
                return BadRequest<string>(_stringLocalizer["UserNotFound"]);

            // Validate password
            var signIn = await _signInManager.CheckPasswordSignInAsync(user, request.Password, false);
            if (!signIn.Succeeded)
                return BadRequest<string>(_stringLocalizer["InvalidCredentials"]);

            // Generate and return JWT token
            var token = await _authenticationService.CreateJWTToken(user);
            return Success<string>(token);
        }
        #endregion
    }
}
