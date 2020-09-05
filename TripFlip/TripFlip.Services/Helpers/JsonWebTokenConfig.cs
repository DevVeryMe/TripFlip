using Microsoft.Extensions.Configuration;

namespace TripFlip.Services.Helpers
{
    public class JsonWebTokenConfig
    {
        private IConfiguration _appConfiguration;

        public string Issuer { get; }

        public string Audience { get; }

        public string SecretKey { get; }

        public int TokenLifetime { get; }

        public JsonWebTokenConfig(IConfiguration appConfiguration)
        {
            _appConfiguration = appConfiguration;

            Issuer = _appConfiguration["Jwt:Issuer"];
            Audience = _appConfiguration["Jwt:Audience"];
            SecretKey = _appConfiguration["Jwt:SecretKey"];
            TokenLifetime = int.Parse(_appConfiguration["Jwt:TokenLifetime"]);
        }
    }
}
