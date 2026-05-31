using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using SchoolProject.Data.Entities.Identity;
using SchoolProject.Data.Helpers;
using SchoolProject.Infrastructure.Abstracts;
using SchoolProject.Service.Abstracts;
using System.Collections.Concurrent;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace SchoolProject.Service.Implementations
{
    public class AuthenticationService : IAuthenticationService
    {
        #region Fields
        private readonly JwtSettings _jwtSettings;
        private readonly ConcurrentDictionary<string, RefreshToken> _refreshTokens = new();
        private readonly IRefreshTokenRepository _refreshTokenRepository;
        private readonly UserManager<AppUser> _userManager;
        #endregion

        #region Constructors
        public AuthenticationService(JwtSettings jwtSettings, IRefreshTokenRepository refreshTokenRepository,
                                     UserManager<AppUser> userManager)
        {
            _jwtSettings = jwtSettings;
            _refreshTokenRepository = refreshTokenRepository;
            _userManager = userManager;
            _refreshTokens = new ConcurrentDictionary<string, RefreshToken>();
        }
        #endregion

        #region Actions
        public Task<JwtAuthenticationResult> GetJWTToken(AppUser user)
        {
            var claims = new List<Claim>
            {
                new Claim("UserClaimModel.Username", user.UserName),
                new Claim("UserClaimModel.Email", user.Email),
                new Claim("UserClaimModel.PhoneNumber", user.PhoneNumber),
                new Claim("UserClaimModel.Id", user.Id.ToString())
            };
            var token = new JwtSecurityToken(
                 _jwtSettings.Issuer,
                 _jwtSettings.Audience,
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(3),
                signingCredentials: new SigningCredentials(new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_jwtSettings.Secret ?? "SchoolProjectSecretKey")), SecurityAlgorithms.HmacSha256)
                );
            var jwtToken = new JwtSecurityTokenHandler().WriteToken(token);
            _refreshTokens[user.UserName] = new RefreshToken
            {
                UserName = user.UserName,
                TokenString = GenerateRefreshToken(),
                ExpirAt = DateTime.UtcNow.AddDays(30)
            };
            return Task.FromResult(new JwtAuthenticationResult { AccessToken = jwtToken, RefreshToken = _refreshTokens[user.UserName] });
        }


        private JwtSecurityToken GenerateJwtToken(AppUser user)
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
                signingCredentials: new SigningCredentials(
                    new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_jwtSettings.Secret ?? "SchoolProjectSecretKey")),
                    SecurityAlgorithms.HmacSha256)
            );

            return token;
        }

        public async Task<JwtAuthenticationResult> GetRefreshToken(string? accessToken, string? refreshToken)
        {
            var jwtToken = ReadJWTToken(accessToken);
            var refresh = _refreshTokens.GetValueOrDefault(refreshToken);
            if (refresh == null || refresh.ExpirAt < DateTime.UtcNow)
                throw new SecurityTokenException("Invalid refresh token.");

            if (jwtToken.ValidTo > DateTime.UtcNow)
                throw new SecurityTokenException("Access token hasn't expired.");

            var userIdClaim = jwtToken.Claims.FirstOrDefault(c => c.Type == "UserClaimModel.Id").Value;
            var userrefreshtoken = await _refreshTokenRepository.GetTableNoTracking().FirstOrDefaultAsync(u => u.RefreshToken == refresh.TokenString
                                                                                        && u.UserId == int.Parse(userIdClaim));
            if (userrefreshtoken == null)
                throw new SecurityTokenException("Refresh token not found in database.");

            if (userrefreshtoken.ExpiryDate < DateTime.UtcNow)
                throw new SecurityTokenException("Refresh token has expired.");

            var user = await _userManager.FindByIdAsync(userIdClaim);
            if (user == null)
                throw new SecurityTokenException("User not found.");

            var newJwtToken = GenerateJwtToken(user);
            var newAccessToken = new JwtSecurityTokenHandler().WriteToken(newJwtToken);

            var response = new JwtAuthenticationResult();
            response.AccessToken = newAccessToken;
            response.RefreshToken = new RefreshToken
            {
                UserName = user.UserName,
                TokenString = GenerateRefreshToken(),
                ExpirAt = DateTime.UtcNow.AddDays(30)
            };
            return response;

        }
        private JwtSecurityToken ReadJWTToken(string? accessToken)
        {
            if (string.IsNullOrEmpty(accessToken))
                throw new ArgumentNullException(nameof(accessToken));

            var handler = new JwtSecurityTokenHandler();
            var response = handler.ReadJwtToken(accessToken);

            return response;
        }

        private string GenerateRefreshToken()
        {
            var randomNumber = new byte[32];
            var rng = RandomNumberGenerator.Create();
            rng.GetBytes(randomNumber);
            return Convert.ToBase64String(randomNumber);
        }

        public async Task<string> ValidateToken(string? accessToken)
        {
            var handler = new JwtSecurityTokenHandler();

            var parameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidIssuer = _jwtSettings.Issuer,
                ValidateAudience = true,
                ValidAudience = _jwtSettings.Audience,
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_jwtSettings.Secret ?? "SchoolProjectSecretKey")),
                ValidateLifetime = false
            };
            var validatorn = handler.ValidateToken(accessToken, parameters, out SecurityToken validatedToken);
            try
            {
                if (validatorn == null)
                    throw new SecurityTokenException("Invalid token: Failed to validate.");
                return "Success";

            }
            catch (Exception ex)
            {
                throw new SecurityTokenException($"Token validation failed: {ex.Message}");
            }

        }
        #endregion
    }
}
