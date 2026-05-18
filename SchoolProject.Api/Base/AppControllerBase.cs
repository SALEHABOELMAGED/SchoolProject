using Microsoft.AspNetCore.Mvc;
using SchoolProject.Core.Bases;
using System.Net;

namespace SchoolProject.Api.Base
{
    // BaseController.cs
    [ApiController]
    public class AppControllerBase : ControllerBase
    {
        // call this in every action instead of repeating the switch
        public IActionResult NewResult<T>(Response<T> response)
        {
            switch (response.StatusCode)
            {
                case HttpStatusCode.OK:
                    return Ok(response);
                case HttpStatusCode.Created:
                    return Created("", response);
                case HttpStatusCode.NotFound:
                    return NotFound(response);
                case HttpStatusCode.Unauthorized:
                    return Unauthorized(response);
                case HttpStatusCode.BadRequest:
                    return BadRequest(response);
                case HttpStatusCode.UnprocessableEntity:
                    return UnprocessableEntity(response);
                case HttpStatusCode.Conflict:
                    return Conflict(response);
                case HttpStatusCode.Accepted:
                    return Accepted("", response);
                default:
                    return Ok(response);
            }
        }
    }
}
