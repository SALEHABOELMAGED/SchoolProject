using MediatR;
using Microsoft.AspNetCore.Mvc;
using SchoolProject.Api.Base;
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

        #endregion

    }
}
