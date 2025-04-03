using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Infrastructure.Authentication;
using Infrastructure.Contract;
using Microsoft.Extensions.Options;
using Core.DTOs;

namespace Infrastructure.Services
{
namespace Infrastructure.Security
    {
        public class JwtTokenService:ITokenService
        {
            private readonly JwtSettings _jwtSettings;

            public JwtTokenService(IOptions<JwtSettings> jwtSettings)
            {
                _jwtSettings = jwtSettings.Value; // Accessing the actual settings object
            }

            public string GenerateToken(UsersWithRole usersWithRole)
            {
                var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.SecretKey));
                var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

                var claims = new List<Claim>();
                claims.Add(new Claim(ClaimTypes.NameIdentifier, usersWithRole.Id.ToString()));

                
          
                if (usersWithRole?.Roles != null)
                {
                    foreach (var role in usersWithRole.Roles)
                    {
                        claims.Add(new Claim(ClaimTypes.Role, role));
                    }
                }

                var token = new JwtSecurityToken(
                    _jwtSettings.Issuer,
                    _jwtSettings.Audience,
                    claims,
                    expires: DateTime.UtcNow.AddHours(1),
                    signingCredentials: credentials
                );

                return new JwtSecurityTokenHandler().WriteToken(token);
            }
        }
    }

}
