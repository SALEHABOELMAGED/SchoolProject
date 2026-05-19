using Microsoft.AspNetCore.Identity;

namespace SchoolProject.Data.Entities.Identity
{
    public class AppUser : IdentityUser<int>
    {
        public string? DisplayName { get; set; }
        public string? Address { get; set; }
        public string? Country { get; set; }
    }
}
