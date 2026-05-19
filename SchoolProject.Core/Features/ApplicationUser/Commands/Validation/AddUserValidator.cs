using FluentValidation;
using Microsoft.Extensions.Localization;
using SchoolProject.Core.Features.User.Commands.Models;
using SchoolProject.Core.Resources;

namespace SchoolProject.Core.Features.User.Commands.Validation
{
    public class AddUserValidator : AbstractValidator<AddUserCommand>
    {
        #region Fields
        private readonly IStringLocalizer<SharedResources> _stringLocalizer;
        #endregion

        #region Constructor
        public AddUserValidator(IStringLocalizer<SharedResources> stringLocalizer)
        {
            _stringLocalizer = stringLocalizer;
            ApplyValidationRules();
            ApplyCustomValidationRules();
        }
        #endregion

        #region Actions
        public void ApplyValidationRules()
        {
            RuleFor(x => x.DisplayName)
                .NotEmpty().WithMessage(_stringLocalizer[SchoolProject.Core.Resources.SharedResourcesKeys.NotEmpty])
                .NotNull().WithMessage(_stringLocalizer[SchoolProject.Core.Resources.SharedResourcesKeys.Required])
                .MaximumLength(100).WithMessage(_stringLocalizer[SchoolProject.Core.Resources.SharedResourcesKeys.NotEmpty]);

            RuleFor(x => x.UserName)
                .NotEmpty().WithMessage(_stringLocalizer[SchoolProject.Core.Resources.SharedResourcesKeys.NotEmpty])
                .NotNull().WithMessage(_stringLocalizer[SchoolProject.Core.Resources.SharedResourcesKeys.Required])
                .MaximumLength(100).WithMessage(_stringLocalizer[SchoolProject.Core.Resources.SharedResourcesKeys.NotEmpty]);

            RuleFor(x => x.Password)
                .NotEmpty().WithMessage(_stringLocalizer[SchoolProject.Core.Resources.SharedResourcesKeys.NotEmpty])
                .NotNull().WithMessage(_stringLocalizer[SchoolProject.Core.Resources.SharedResourcesKeys.Required])
                .MaximumLength(100).WithMessage(_stringLocalizer[SchoolProject.Core.Resources.SharedResourcesKeys.NotEmpty]);

            RuleFor(x => x.ConfirmPassword)
               .Equal(x => x.Password).WithMessage("Confirm Password must match Password.");

            RuleFor(x => x.Address)
                .NotEmpty().WithMessage("[PropertyName]Address is required.")
                .NotNull().WithMessage("[PropertyValue]Address cannot be null.");

            RuleFor(x => x.Email)
              .NotEmpty().WithMessage(_stringLocalizer[SchoolProject.Core.Resources.SharedResourcesKeys.NotEmpty])
              .NotNull().WithMessage(_stringLocalizer[SchoolProject.Core.Resources.SharedResourcesKeys.Required]);
        }
        public void ApplyCustomValidationRules()
        {
            //RuleFor(x => x.Email)
            //    .MustAsync(async (Key, CancellationToken) => !await _userService.IsEmailExist(Key))
            //    .WithMessage("User with the same email already exists.");

            //RuleFor(x => x.DisplayName)
            //    .MustAsync(async (Key, CancellationToken) => await _userService.IsDisplayNameExist(Key))
            //    .WithMessage("Display Name does not exist.");
        }
        #endregion

    }

}
