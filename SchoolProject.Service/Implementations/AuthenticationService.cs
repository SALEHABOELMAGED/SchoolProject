using Microsoft.IdentityModel.Tokens;
using SchoolProject.Data.Entities.Identity;
using SchoolProject.Data.Helpers;
using SchoolProject.Service.Abstracts;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace SchoolProject.Service.Implementations
{
    public class AuthenticationService : IAuthenticationService
    {
        #region Fields
        private readonly JwtSettings _jwtSettings;
        #endregion

        #region Constructors
        public AuthenticationService(JwtSettings jwtSettings)
        {
            _jwtSettings = jwtSettings;
        }
        #endregion

        #region Actions
        public Task<string> CreateJWTToken(AppUser user)
        {
            var claims = new List<Claim>
            {
                new Claim("UserClaimModel.Username", user.UserName),
                new Claim("UserClaimModel.Email", user.Email),
                new Claim("UserClaimModel.PhoneNumber", user.PhoneNumber)
            };
            var token = new JwtSecurityToken(
                 _jwtSettings.Issuer,
                 _jwtSettings.Audience,
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(3),
                signingCredentials: new SigningCredentials(new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_jwtSettings.Secret ?? "SchoolProjectSecretKey")), SecurityAlgorithms.HmacSha256)
                );
            return Task.FromResult(new JwtSecurityTokenHandler().WriteToken(token));
        }
        #endregion
    }
}
