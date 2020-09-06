using Microsoft.AspNetCore.Http;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;

namespace TripFlip.Services.Helpers
{
    static class JwtHeaderParseHelper
    {
        /// <summary>
        /// Parses http request header and decodes JWT.
        /// </summary>
        /// <param name="httpRequest">IHeaderDictionary instance.</param>
        /// <returns>Decoded JWT</returns>
        public static JwtSecurityToken ParseHeader(IHeaderDictionary headers)
        {
            // Getting encoded JWT without "Bearer " substring.
            var encodedToken = headers
                .FirstOrDefault(header => header.Key == "Authorization").Value[0].Substring(7);

            var jwtHandler = new JwtSecurityTokenHandler();
            var token = jwtHandler.ReadJwtToken(encodedToken);

            return token;
        }
    }
}
