using SchoolProject.Core.Features.ApplicationUser.Commands.Models;

namespace SchoolProject.Core.Mapping.AppUser
{
    public partial class AppUserProfile
    {
        public void UpdateUserCommandMapping()
        {
            CreateMap<UpdateUserCommand, SchoolProject.Data.Entities.Identity.AppUser>();

        }
    }
}
