using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using TripFlip.Domain.Entities;

namespace TripFlip.Services.Helpers
{
    public static class JsonWebTokenHelper
    {
        /// <summary>
        /// Generates JWT.
        /// </summary>
        /// <param name="userIncludingRoles">User entity with included roles.</param>
        /// <param name="issuer">Issuer of the JWT.</param>
        /// <param name="audience">Audience of the JWT.</param>
        /// <param name="secretKey">JWT secret key.</param>
        /// <param name="tokenLifetime">JWT lifetime.</param>
        /// <returns>Encoded JWT.</returns>
        public static string GenerateJsonWebToken(
            UserEntity userIncludingRoles,
            string issuer,
            string audience,
            string secretKey,
            int tokenLifetime)
        {
            var encodedSecretKey = new SymmetricSecurityKey(
                Encoding.ASCII.GetBytes(secretKey));
            var credentials = new SigningCredentials(
            encodedSecretKey,
            SecurityAlgorithms.HmacSha256
            );

            int expirationTime = tokenLifetime;

            var roles = userIncludingRoles.ApplicationRoles
                .Select(role => new Claim(ClaimTypes.Role, role.ApplicationRole.Name));

            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub, userIncludingRoles.Id.ToString()),
                new Claim(JwtRegisteredClaimNames.Email, userIncludingRoles.Email)
            };

            claims.AddRange(roles);

            // creating JWT
            var jwt = new JwtSecurityToken(
                issuer: issuer,
                audience: audience,
                claims: claims,
                expires: DateTime.Now.AddMinutes(expirationTime),
                signingCredentials: credentials
                );

            string encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);

            return encodedJwt;
        }
    }
}
