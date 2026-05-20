using MediatR;
using SchoolProject.Core.Bases;
using SchoolProject.Core.Features.ApplicationUser.Queries.Results;
using SchoolProject.Core.Wrappers;

namespace SchoolProject.Core.Features.ApplicationUser.Queries.Models
{
    public class GetUserPaginationQuery : IRequest<Response<PaginatedResult<GetUserPaginationQueryResult>>>
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
    }
}
