using Newtonsoft.Json;

namespace TripFlip.Services.Models
{
    /// <summary>
    /// Helper class that describes useful values of Google's 
    /// <see href="https://accounts.google.com/.well-known/openid-configuration">
    /// Discovery document</see> (OpenID configuration).
    /// </summary>
    public class GoogleOpenIdConfiguration
    {
        [JsonProperty(PropertyName = "issuer")]
        public string Issuer { get; set; }

        [JsonProperty(PropertyName = "authorization_endpoint")]
        public string AuthorizationEndpoint { get; set; }

        [JsonProperty(PropertyName = "token_endpoint")]
        public string TokenEndpoint { get; set; }

        [JsonProperty(PropertyName = "userinfo_endpoint")]
        public string UserinfoEndpoint { get; set; }

        [JsonProperty(PropertyName = "response_types_supported")]
        public string[] ResponseTypesSupported { get; set; }

        [JsonProperty(PropertyName = "scopes_supported")]
        public string[] ScopesSupported { get; set; }

        [JsonProperty(PropertyName = "grant_types_supported")]
        public string[] GrantTypesSupported { get; set; }
    }
}
