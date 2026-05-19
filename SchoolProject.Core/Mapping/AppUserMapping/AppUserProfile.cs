using AutoMapper;

namespace SchoolProject.Core.Mapping.AppUser
{
    public partial class AppUserProfile : Profile
    {
        public AppUserProfile()
        {
            AddUserCommandMapping();
        }
    }
}
