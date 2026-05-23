using MediatR;
using Microsoft.AspNetCore.Mvc;
using SchoolProject.Api.Base;
using SchoolProject.Core.Features.ApplicationUser.Commands.Models;
using SchoolProject.Core.Features.ApplicationUser.Queries.Models;
using SchoolProject.Core.Features.User.Commands.Models;
using SchoolProject.Data.AppMetaData;

namespace SchoolProject.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AppUserController : AppControllerBase
    {
        #region Fields
        private readonly IMediator _mediator;
        #endregion

        #region Constructors
        public AppUserController(IMediator mediator)
        {
            _mediator = mediator;
        }
        #endregion

        #region Handle Functions
        [HttpPost(Routes.AppUserRouting.AddUser)]
        public async Task<IActionResult> AddUserAsync([FromBody] AddUserCommand command)
        {
            return NewResult(await _mediator.Send(command));
        }

        [HttpGet(Routes.AppUserRouting.Paginated)]
        public async Task<IActionResult> GetUserPaginationAsync([FromQuery] GetUserPaginationQuery query)
        {
            return NewResult(await _mediator.Send(query));
        }

        [HttpGet(Routes.AppUserRouting.GetUserById)]
        public async Task<IActionResult> GetUserByIdAsync([FromRoute] int id)
        {
            return NewResult(await _mediator.Send(new GetUserByIdQuery { Id = id }));
        }
        [HttpPut(Routes.AppUserRouting.UpdateUser)]
        public async Task<IActionResult> UpdateUserCommand([FromRoute] int id, [FromBody] UpdateUserCommand command)
        {
            command.Id = id;
            return NewResult(await _mediator.Send(command));
        }

        [HttpDelete(Routes.AppUserRouting.DeleteUser)]
        public async Task<IActionResult> DeleteUserCommand([FromRoute] int id, [FromBody] DeleteUserCommand command)
        {
            command.Id = id;
            return NewResult(await _mediator.Send(command));
        }

        [HttpPut(Routes.AppUserRouting.ChangePassword)]
        public async Task<IActionResult> ChangeUserPasswordCommand([FromRoute] int id, [FromBody] ChangeUserPasswordCommand command)
        {
            command.Id = id;
            return NewResult(await _mediator.Send(command));
        }

        #endregion

    }
}
