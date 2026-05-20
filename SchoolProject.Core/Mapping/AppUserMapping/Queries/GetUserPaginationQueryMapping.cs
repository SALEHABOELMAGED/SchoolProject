using SchoolProject.Core.Features.ApplicationUser.Queries.Results;

namespace SchoolProject.Core.Mapping.AppUser
{
    public partial class AppUserProfile
    {
        public void GetPaginationMapping()
        {
            CreateMap<GetUserPaginationQueryResult, SchoolProject.Data.Entities.Identity.AppUser>()
                .ForMember(dest => dest.DisplayName, opt => opt.MapFrom(src => src.FullName))
                .ForMember(dest => dest.Address, opt => opt.MapFrom(src => src.Address))
                .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email))
                .ForMember(dest => dest.Country, opt => opt.MapFrom(src => src.Country));
        }
    }
}
