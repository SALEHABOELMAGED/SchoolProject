using SchoolProject.Data.Entities.Identity;
using SchoolProject.Data.Helpers;

namespace SchoolProject.Service.Abstracts
{
    public interface IAuthenticationService
    {
        public Task<JwtAuthenticationResult> GetJWTToken(AppUser user);
        public Task<JwtAuthenticationResult> GetRefreshToken(string? accessToken, string? refreshToken);
        public Task<string> ValidateToken(string? accessToken);
    }
}
