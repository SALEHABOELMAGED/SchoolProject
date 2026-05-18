using FluentValidation;
using Microsoft.Extensions.Localization;
using SchoolProject.Core.Features.Students.Commands.Models;
using SchoolProject.Service.Abstracts;

namespace SchoolProject.Core.Features.Students.Commands.Validations
{
    public class EditStudentValidator : AbstractValidator<EditStudentCommand>
    {
        #region Fields
        private readonly IStudentService _studentService;
        private readonly IStringLocalizer<SchoolProject.Core.Resources.SharedResources> _stringLocalizer;
        #endregion

        #region Constructors
        public EditStudentValidator(IStudentService studentService, IStringLocalizer<SchoolProject.Core.Resources.SharedResources> stringLocalizer)
        {
            _studentService = studentService;
            _stringLocalizer = stringLocalizer;
            ApplyValidationRules();
            ApplyCustomValidationRules();

        }
        #endregion

        #region Actions
        public void ApplyValidationRules()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage(_stringLocalizer[SchoolProject.Core.Resources.SharedResourcesKeys.NotEmpty])
                .NotNull().WithMessage(_stringLocalizer[SchoolProject.Core.Resources.SharedResourcesKeys.NotEmpty])
                .MaximumLength(100).WithMessage(_stringLocalizer[SchoolProject.Core.Resources.SharedResourcesKeys.NotEmpty]);

            RuleFor(x => x.Address)
                .NotEmpty().WithMessage(_stringLocalizer[SchoolProject.Core.Resources.SharedResourcesKeys.NotEmpty])
                .NotNull().WithMessage(_stringLocalizer[SchoolProject.Core.Resources.SharedResourcesKeys.NotEmpty]);
        }
        public void ApplyCustomValidationRules()
        {
            RuleFor(x => x.Name)
                .MustAsync(async (model, name, CancellationToken) => !await _studentService.IsNameExistExcludeSelf(name, model.Id))
                .WithMessage("Student with the same name already exists.");
        }
        #endregion
    }
}
