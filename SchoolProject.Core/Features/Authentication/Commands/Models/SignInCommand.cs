using MediatR;
using SchoolProject.Core.Bases;
using SchoolProject.Data.Helpers;

namespace SchoolProject.Core.Features.Authentication.Commands.Models
{
    public class SignInCommand : IRequest<Response<JwtAuthenticationResult>>
    {
        public string? Username { get; set; }
        public string? Password { get; set; }

    }
}
