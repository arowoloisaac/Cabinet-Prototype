using Cabinet_Prototype.Configurations;
using Cabinet_Prototype.Models;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Cabinet_Prototype.Services.TokenService
{
    public class TokenGenerator : ITokenGenerator
    {
        private readonly JwtBearerSetting _tokenSetting;

        public TokenGenerator(IOptions<JwtBearerSetting> jwtTokenOptions)
        {
            _tokenSetting = jwtTokenOptions.Value;
        }

        public string GenerateToken(User user, IList<string> roles)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_tokenSetting.SecretKey);

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.Authentication, user.Id.ToString()),
            };

            foreach (var role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }

            var descriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddSeconds(_tokenSetting.ExpiryTimeInSeconds),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature),
                Audience = _tokenSetting.Audience,
                Issuer = _tokenSetting.Issuer,
            };

            var token = tokenHandler.CreateToken(descriptor);

            return tokenHandler.WriteToken(token);
        }
    }
}
