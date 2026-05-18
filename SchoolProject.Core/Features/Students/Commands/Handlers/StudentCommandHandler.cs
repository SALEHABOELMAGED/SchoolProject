using AutoMapper;
using MediatR;
using Microsoft.Extensions.Localization;
using SchoolProject.Core.Bases;
using SchoolProject.Core.Features.Students.Commands.Models;
using SchoolProject.Data.Entities;
using SchoolProject.Service.Abstracts;

namespace SchoolProject.Core.Features.Students.Commands.Handlers
{
    public class StudentCommandHandler : ResponseHandler,
                                         IRequestHandler<AddStudentCommand, Response<string>>,
                                         IRequestHandler<EditStudentCommand, Response<string>>,
                                         IRequestHandler<DeleteStudentCommand, Response<string>>
    {
        #region Fields
        private readonly IStudentService _studentService;
        private readonly IMapper _mapper;
        private readonly IStringLocalizer<SchoolProject.Core.Resources.SharedResources> _stringLocalizer;
        #endregion

        #region Constructors
        public StudentCommandHandler(IStudentService studentService, IMapper mapper, IStringLocalizer<SchoolProject.Core.Resources.SharedResources> stringLocalizer)
        {
            _studentService = studentService;
            _mapper = mapper;
            _stringLocalizer = stringLocalizer;
        }
        #endregion

        #region Handle Functions
        public async Task<Response<string>> Handle(AddStudentCommand request, CancellationToken cancellationToken)
        {
            var studentmapper = _mapper.Map<Student>(request);
            var result = await _studentService.AddAsync(studentmapper);

            if (result == "Success")
            {
                return Created<string>("Added Successfully.");
            }

            else return BadRequest<string>(result);
        }

        public async Task<Response<string>> Handle(EditStudentCommand request, CancellationToken cancellationToken)
        {
            var findstudent = await _studentService.GetStudentByIdAsync(request.Id);

            if (findstudent == null)
                return NotFound<string>("Student not found.");

            var studentmapper = _mapper.Map(request, findstudent);

            var result = await _studentService.EditAsync(studentmapper);

            if (result == "Success")
                return Success<string>($"Id {studentmapper.StudentId} Edited Successfully.");
            else
                return BadRequest<string>(result);
        }

        public async Task<Response<string>> Handle(DeleteStudentCommand request, CancellationToken cancellationToken)
        {
            var findstudent = await _studentService.GetStudentByIdAsync(request.Id);

            if (findstudent == null)
                return NotFound<string>("Student not found.");

            var result = await _studentService.DeleteAsync(findstudent);

            if (result == "Success")
                return Success<string>($"Id {request.Id} Deleted Successfully.");
            else
                return BadRequest<string>(result);
        }
        #endregion
    }
}
