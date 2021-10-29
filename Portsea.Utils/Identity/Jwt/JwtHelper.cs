using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace Portsea.Utils.Identity.Jwt
{
    public class JwtHelper
    {
        public static JwtSecurityToken GetJwtToken(
            string username,
            string issuer,
            string audience,
            string issuerKey,
            TimeSpan expiration,
            IEnumerable<Claim> additionalClaims = null)
        {
            List<Claim> claims = new List<Claim>()
            {
                new Claim(JwtRegisteredClaimNames.Sub, username),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            };

            if (additionalClaims is object)
            {
                claims.AddRange(additionalClaims);
            }

            var key = GetSymetricSecurityKey(issuerKey);
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            DateTime notBefore = DateTime.UtcNow;
            return new JwtSecurityToken(
                issuer: issuer,
                audience: audience,
                notBefore: notBefore,
                expires: notBefore.Add(expiration),
                claims: claims,
                signingCredentials: creds);
        }

        public static string GetJwtTokenString(JwtSecurityToken token)
        {
            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public static SymmetricSecurityKey GetSymetricSecurityKey(string issuerKey)
        {
            return new SymmetricSecurityKey(Encoding.UTF8.GetBytes(issuerKey));
        }
    }
}