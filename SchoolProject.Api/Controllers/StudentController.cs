using MediatR;
using Microsoft.AspNetCore.Mvc;
using SchoolProject.Api.Base;
using SchoolProject.Core.Features.Students.Commands.Models;
using SchoolProject.Core.Features.Students.Queries.Models;
using SchoolProject.Data.AppMetaData;

namespace SchoolProject.Api.Controllers
{

    public class StudentController : AppControllerBase
    {
        #region Fields
        private readonly IMediator _mediator;
        #endregion

        #region Constructors
        public StudentController(IMediator mediator)
        {
            _mediator = mediator;
        }
        #endregion

        #region Handle Functions
        [HttpGet(Routes.StudentRouting.GetStudentsList)]
        public async Task<IActionResult> GetStudentsList()
        {
            var result = await _mediator.Send(new GetStudentListQuery());
            return NewResult(result);
        }

        [HttpGet(Routes.StudentRouting.Paginated)]
        public async Task<IActionResult> Paginated([FromQuery] GetStudentPaginatedListQuery query)
        {
            var result = await _mediator.Send(query);
            return Ok(result);
        }

        [HttpGet(Routes.StudentRouting.GetStudentById)]
        public async Task<IActionResult> GetStudentById([FromRoute] int id)
        {
            var result = await _mediator.Send(new GetStudentByIdQuery { Id = id });
            return NewResult(result);
        }
        [HttpPost(Routes.StudentRouting.Create)]
        public async Task<IActionResult> CreateStudent([FromBody] AddStudentCommand command)
        {
            var result = await _mediator.Send(command);
            return NewResult(result);
        }
        [HttpPut(Routes.StudentRouting.Update)]
        public async Task<IActionResult> UpdateStudent([FromBody] EditStudentCommand command)
        {
            var result = await _mediator.Send(command);
            return NewResult(result);
        }

        [HttpDelete(Routes.StudentRouting.Delete)]
        public async Task<IActionResult> DeleteStudent([FromRoute] int id)
        {
            return NewResult(await _mediator.Send(new DeleteStudentCommand(id)));
        }

        #endregion
    }
}
