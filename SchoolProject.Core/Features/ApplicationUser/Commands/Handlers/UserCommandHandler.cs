using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Localization;
using SchoolProject.Core.Bases;
using SchoolProject.Core.Features.ApplicationUser.Commands.Models;
using SchoolProject.Core.Features.User.Commands.Models;
using SchoolProject.Core.Resources;
using SchoolProject.Data.Entities.Identity;

namespace SchoolProject.Core.Features.User.Commands.Handlers
{
    public class UserCommandHandler : ResponseHandler,
        IRequestHandler<AddUserCommand, Response<string>>,
        IRequestHandler<UpdateUserCommand, Response<string>>,
        IRequestHandler<DeleteUserCommand, Response<string>>,
        IRequestHandler<ChangeUserPasswordCommand, Response<string>>,
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

        public async Task<Response<string>> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByIdAsync(request.Id.ToString());
            if (user != null)
            {
                var emailUser = await _userManager.FindByEmailAsync(request.Email);
                if (emailUser != null && emailUser.Id != request.Id) return BadRequest<string>(_stringlocalizer["Email Already Found"]);
                var nameUser = await _userManager.FindByNameAsync(request.UserName);
                if (nameUser != null && nameUser.Id != request.Id) return BadRequest<string>(_stringlocalizer["User Name Already Exists"]);
            }
            if (user == null) return BadRequest<string>(_stringlocalizer["User Not Found"]);

            _mapper.Map(request, user);
            var result = await _userManager.UpdateAsync(user);
            if (!result.Succeeded) return BadRequest<string>(string.Join(", ", result.Errors.Select(e => e.Description)));

            return Success<string>(_stringlocalizer["User Updated Successfully"]);
        }

        public async Task<Response<string>> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
        {
            var findUser = await _userManager.FindByIdAsync(request.Id.ToString());

            if (findUser == null)
                return NotFound<string>("User not found.");

            var result = await _userManager.DeleteAsync(findUser);

            if (result.Succeeded)
                return Success<string>($"Id {request.Id} Deleted Successfully.");
            else
                return BadRequest<string>(string.Join(", ", result.Errors.Select(e => e.Description)));
        }

        public async Task<Response<string>> Handle(ChangeUserPasswordCommand request, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByIdAsync(request.Id.ToString());

            if (user == null) return BadRequest<string>(_stringlocalizer["User Not Found"]);

            var result = await _userManager.ChangePasswordAsync(user, request.OldPassword, request.NewPassword);

            if (!result.Succeeded) return BadRequest<string>(string.Join(", ", result.Errors.Select(e => e.Description)));
            return Success<string>(_stringlocalizer["Password Changed Successfully"]);

        }
        #endregion

        #region Actions

        #endregion
    }
}
