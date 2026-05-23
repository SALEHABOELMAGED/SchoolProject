using FluentValidation;
using Microsoft.Extensions.Localization;
using SchoolProject.Core.Features.Authentication.Commands.Models;

namespace SchoolProject.Core.Features.Authentication.Commands.Validations
{
    public class SignInValidator : AbstractValidator<SignInCommand>
    {
        #region Fields
        private readonly IStringLocalizer<SchoolProject.Core.Resources.SharedResources> _stringLocalizer;

        #endregion

        #region Constructors
        public SignInValidator(IStringLocalizer<SchoolProject.Core.Resources.SharedResources> stringLocalizer)
        {
            _stringLocalizer = stringLocalizer;
            ApplyValidationRules();
        }
        #endregion

        #region Actions
        public void ApplyValidationRules()
        {
            RuleFor(x => x.Username)
                .NotEmpty().WithMessage(_stringLocalizer[SchoolProject.Core.Resources.SharedResourcesKeys.NotEmpty])
                .NotNull().WithMessage(_stringLocalizer[SchoolProject.Core.Resources.SharedResourcesKeys.Required]);

            RuleFor(x => x.Password)
                .NotEmpty().WithMessage(_stringLocalizer[SchoolProject.Core.Resources.SharedResourcesKeys.NotEmpty])
                .NotNull().WithMessage(_stringLocalizer[SchoolProject.Core.Resources.SharedResourcesKeys.Required]);
        }
        #endregion
    }
}
