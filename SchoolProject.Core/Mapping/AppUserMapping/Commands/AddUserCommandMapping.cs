using SchoolProject.Core.Features.User.Commands.Models;

namespace SchoolProject.Core.Mapping.AppUser
{
    public partial class AppUserProfile
    {
        public void AddUserCommandMapping()
        {
            CreateMap<AddUserCommand, SchoolProject.Data.Entities.Identity.AppUser>()
                .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.UserName))
                .ForMember(dest => dest.DisplayName, opt => opt.MapFrom(src => src.DisplayName))
                .ForMember(dest => dest.Address, opt => opt.MapFrom(src => src.Address))
                .ForMember(dest => dest.Country, opt => opt.MapFrom(src => src.Country))
                .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email))
                .ForMember(dest => dest.PhoneNumber, opt => opt.MapFrom(src => src.PhoneNummber));
        }
    }
}
