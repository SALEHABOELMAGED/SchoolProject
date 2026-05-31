using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;

namespace SchoolProject.Data.Entities.Identity
{
    public class AppUser : IdentityUser<int>
    {
        public AppUser()
        {
            UserRefreshTokens = new HashSet<UserRefreshToken>();
        }
        public string? DisplayName { get; set; }
        public string? Address { get; set; }
        public string? Country { get; set; }

        [InverseProperty(nameof(UserRefreshToken.User))]
        public virtual ICollection<UserRefreshToken>? UserRefreshTokens { get; set; }
    }
}
