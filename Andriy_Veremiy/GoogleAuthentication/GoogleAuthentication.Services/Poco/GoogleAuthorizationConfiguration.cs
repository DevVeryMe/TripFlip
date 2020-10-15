using System.Collections.Generic;

namespace GoogleAuthentication.Services.Poco
{
    public class GoogleAuthorizationConfiguration
    {
        public string ClientId { get; set; }

        public string ClientSecret { get; set; }

        public string RedirectUri { get; set; }
    }
}
