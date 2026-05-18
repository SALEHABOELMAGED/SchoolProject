using FluentValidation;
using Microsoft.Extensions.Localization;
using SchoolProject.Core.Features.Students.Commands.Models;
using SchoolProject.Service.Abstracts;

namespace SchoolProject.Core.Features.Students.Commands.Validations
{
    public class AddStudentValidator : AbstractValidator<AddStudentCommand>
    {
        #region Fields
        private readonly IStudentService _studentService;
        private readonly IStringLocalizer<SchoolProject.Core.Resources.SharedResources> _stringLocalizer;
        private readonly IDepartmentService _departmentService;
        #endregion

        #region Constructors
        public AddStudentValidator(IStudentService studentService, IStringLocalizer<SchoolProject.Core.Resources.SharedResources> stringLocalizer, IDepartmentService departmentService)
        {
            _studentService = studentService;
            _stringLocalizer = stringLocalizer;
            _departmentService = departmentService;
            ApplyValidationRules();
            ApplyCustomValidationRules();

        }
        #endregion

        #region Actions
        public void ApplyValidationRules()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage(_stringLocalizer[SchoolProject.Core.Resources.SharedResourcesKeys.NotEmpty])
                .NotNull().WithMessage(_stringLocalizer[SchoolProject.Core.Resources.SharedResourcesKeys.Required])
                .MaximumLength(100).WithMessage(_stringLocalizer[SchoolProject.Core.Resources.SharedResourcesKeys.NotEmpty]);

            RuleFor(x => x.Address)
                .NotEmpty().WithMessage("[PropertyName]Address is required.")
                .NotNull().WithMessage("[PropertyValue]Address cannot be null.");

            RuleFor(x => x.DepartmentId)
              .NotEmpty().WithMessage(_stringLocalizer[SchoolProject.Core.Resources.SharedResourcesKeys.NotEmpty])
              .NotNull().WithMessage(_stringLocalizer[SchoolProject.Core.Resources.SharedResourcesKeys.Required]);
        }
        public void ApplyCustomValidationRules()
        {
            RuleFor(x => x.Name)
                .MustAsync(async (Key, CancellationToken) => !await _studentService.IsNameExist(Key))
                .WithMessage("Student with the same name already exists.");

            RuleFor(x => x.DepartmentId)
                .MustAsync(async (Key, CancellationToken) => await _departmentService.IsDepartmentIdExist(Key))
                .WithMessage("Department ID does not exist.");
        }
        #endregion

    }
}
