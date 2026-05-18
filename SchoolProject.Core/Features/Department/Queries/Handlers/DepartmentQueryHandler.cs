using AutoMapper;
using MediatR;
using Microsoft.Extensions.Localization;
using SchoolProject.Core.Bases;
using SchoolProject.Core.Features.Department.Queries.Models;
using SchoolProject.Core.Features.Department.Queries.Results;
using SchoolProject.Core.Wrappers;
using SchoolProject.Data.Entities;
using SchoolProject.Service.Abstracts;
using System.Linq.Expressions;

namespace SchoolProject.Core.Features.Department.Queries.Handlers
{
    public class DepartmentQueryHandler : ResponseHandler,
        IRequestHandler<GetDepartmentByIdQuery, Response<GetDepartmentByIdResponse>>
    {
        #region Fields
        private readonly IDepartmentService _departmentService;
        private readonly IStringLocalizer<SchoolProject.Core.Resources.SharedResources> _stringLocalizer;
        private readonly IStudentService _studentService;
        private readonly IMapper _mapper;
        #endregion

        #region Constructors
        public DepartmentQueryHandler(IStringLocalizer<SchoolProject.Core.Resources.SharedResources> stringLocalizer,
                                      IDepartmentService departmentService,
                                      IMapper mapper,
                                      IStudentService studentService)
        {
            _departmentService = departmentService;
            _stringLocalizer = stringLocalizer;
            _mapper = mapper;
            _studentService = studentService;
        }
        #endregion

        #region Handle Functions
        public async Task<Response<GetDepartmentByIdResponse>> Handle(GetDepartmentByIdQuery request, CancellationToken cancellationToken)
        {
            var department = await _departmentService.GetDepartmentByIdAsync(request.Id);
            if (department == null)
                return NotFound<GetDepartmentByIdResponse>(_stringLocalizer[SchoolProject.Core.Resources.SharedResourcesKeys.NotFound]);

            var result = _mapper.Map<GetDepartmentByIdResponse>(department);
            Expression<Func<Student, StudentResponse>> expression = e => new StudentResponse
            {
                Id = e.StudentId,
                Name = e.Name
            };
            var studentQurable = _studentService.GetStudentsByDepartmentIdAsQueryable(request.Id);
            var paginatedList = await studentQurable.Select(expression).ToPaginatedResultAsync(request.StudentPageNumber, request.StudentPageSize);
            result.StudentList = paginatedList;
            return Success(result);
        }
        #endregion
    }
}
