using MediatR;
using Microsoft.AspNetCore.Mvc;
using SchoolProject.Api.Base;
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

        #endregion

    }
}
