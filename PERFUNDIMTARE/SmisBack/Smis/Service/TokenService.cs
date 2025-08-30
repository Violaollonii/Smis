using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Smis.Interfaces;
using Smis.Models;

namespace Smis.Service
{
    public class TokenService : ITokenService
    {
        private readonly IConfiguration _config;
        private readonly SymmetricSecurityKey _key;
        private readonly UserManager<Useri> _userManager;

        public TokenService(IConfiguration config, UserManager<Useri> userManager)
        {
            _config = config;
            _userManager = userManager;

            var signingKey = _config["JWT:SigningKey"];
            if (string.IsNullOrWhiteSpace(signingKey))
                throw new InvalidOperationException("JWT:SigningKey missing");

            _key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(signingKey));
        }

        public async Task<List<Claim>> BuildClaimsAsync(Useri user)
        {
            var claims = new List<Claim>
            {
                // ✅ Make sure ASP.NET can read the user id:
                new Claim(ClaimTypes.NameIdentifier, user.Id),
                new Claim(ClaimTypes.Name, user.UserName ?? user.Email ?? user.Id),

                // Optional but nice to have for JWT tooling:
                new Claim(JwtRegisteredClaimNames.Email, user.Email ?? string.Empty),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.NameId, user.Id) // keeps "nameid" too
            };

            var roles = await _userManager.GetRolesAsync(user);
            foreach (var role in roles)
                claims.Add(new Claim(ClaimTypes.Role, role));

            return claims;
        }

        public string CreateToken(Useri user)
        {
            // keep CreateToken synchronous if you want; otherwise make it async and await BuildClaimsAsync
            var claims = BuildClaimsAsync(user).GetAwaiter().GetResult();

            var creds = new SigningCredentials(_key, SecurityAlgorithms.HmacSha512Signature);

            var issuer   = _config["JWT:Issuer"];
            var audience = _config["JWT:Audience"];
            if (string.IsNullOrWhiteSpace(issuer))   throw new InvalidOperationException("JWT:Issuer missing");
            if (string.IsNullOrWhiteSpace(audience)) throw new InvalidOperationException("JWT:Audience missing");

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddMinutes(30),
                Issuer = issuer,
                Audience = audience,
                SigningCredentials = creds
            };

            var handler = new JwtSecurityTokenHandler();
            var token = handler.CreateToken(tokenDescriptor);
            return handler.WriteToken(token);
        }

        public RefreshToken GenerateRefreshToken()
        {
            var bytes = RandomNumberGenerator.GetBytes(64);
            return new RefreshToken
            {
                Token = Convert.ToBase64String(bytes),
                Expires = DateTime.UtcNow.AddDays(2),
                Created = DateTime.UtcNow
            };
        }
    }
}
