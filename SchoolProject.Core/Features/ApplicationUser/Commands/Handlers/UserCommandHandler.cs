using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Localization;
using SchoolProject.Core.Bases;
using SchoolProject.Core.Features.User.Commands.Models;
using SchoolProject.Core.Resources;
using SchoolProject.Data.Entities.Identity;

namespace SchoolProject.Core.Features.User.Commands.Handlers
{
    public class UserCommandHandler : ResponseHandler,
        IRequestHandler<AddUserCommand, Response<string>>,
        IStringLocalizer<SharedResources>
    {
        #region Fields
        private readonly IStringLocalizer<SharedResources> _stringlocalizer;
        private readonly IMapper _mapper;
        private readonly UserManager<AppUser> _userManager;

        public UserCommandHandler(IStringLocalizer<SharedResources> stringlocalizer, IMapper mapper, UserManager<AppUser> userManager) : base(stringlocalizer)
        {
            _stringlocalizer = stringlocalizer;
            _mapper = mapper;
            _userManager = userManager;
        }

        public LocalizedString this[string name] => throw new NotImplementedException();

        public LocalizedString this[string name, params object[] arguments] => throw new NotImplementedException();

        public IEnumerable<LocalizedString> GetAllStrings(bool includeParentCultures)
        {
            throw new NotImplementedException();
        }
        #endregion

        #region Constructor
        public async Task<Response<string>> Handle(AddUserCommand request, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByEmailAsync(request.Email);
            if (user != null) return BadRequest<string>(_stringlocalizer["Email Already Found"]);
            var name = await _userManager.FindByNameAsync(request.UserName);
            if (name != null) return BadRequest<string>(_stringlocalizer["User Name Already Exists"]);

            var mappedUser = _mapper.Map<AppUser>(request);
            var result = await _userManager.CreateAsync(mappedUser, request.Password);
            if (!result.Succeeded) return BadRequest<string>(string.Join(", ", result.Errors.Select(e => e.Description)));

            return Success<string>(_stringlocalizer["User Added Successfully"]);
        }
        #endregion

        #region Actions

        #endregion
    }
}
