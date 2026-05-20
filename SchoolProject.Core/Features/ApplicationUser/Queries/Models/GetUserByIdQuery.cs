using MediatR;
using SchoolProject.Core.Bases;
using SchoolProject.Core.Features.ApplicationUser.Queries.Results;

namespace SchoolProject.Core.Features.ApplicationUser.Queries.Models
{
    public class GetUserByIdQuery : IRequest<Response<GetUserByIdQueryResult>>
    {
        public int Id { get; set; }
    }
}
