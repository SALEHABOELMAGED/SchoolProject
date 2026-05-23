using FluentValidation;
using Microsoft.Extensions.Localization;
using SchoolProject.Core.Features.ApplicationUser.Commands.Models;
using SchoolProject.Core.Resources;

namespace SchoolProject.Core.Features.ApplicationUser.Commands.Validation
{
    public class ChangeUserPasswordValidator : AbstractValidator<ChangeUserPasswordCommand>
    {
        #region Fields
        private readonly IStringLocalizer<SharedResources> _stringLocalizer;
        #endregion

        #region Constructor
        public ChangeUserPasswordValidator(IStringLocalizer<SharedResources> stringLocalizer)
        {
            _stringLocalizer = stringLocalizer;
            ApplyValidationRules();
            ApplyCustomValidationRules();
        }
        #endregion

        #region Actions
        public void ApplyValidationRules()
        {


            RuleFor(x => x.OldPassword)
                .NotEmpty().WithMessage(_stringLocalizer[SchoolProject.Core.Resources.SharedResourcesKeys.NotEmpty])
                .NotNull().WithMessage(_stringLocalizer[SchoolProject.Core.Resources.SharedResourcesKeys.Required]);

            RuleFor(x => x.NewPassword)
                .NotEmpty().WithMessage(_stringLocalizer[SchoolProject.Core.Resources.SharedResourcesKeys.NotEmpty])
                .NotNull().WithMessage(_stringLocalizer[SchoolProject.Core.Resources.SharedResourcesKeys.Required]);

            RuleFor(x => x.ConfirmNewPassword)
               .Equal(x => x.NewPassword).WithMessage("Confirm Password must match New Password.");

        }
        public void ApplyCustomValidationRules()
        {

        }
        #endregion
    }
}
