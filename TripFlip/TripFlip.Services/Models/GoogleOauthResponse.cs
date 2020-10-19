using Newtonsoft.Json;

namespace TripFlip.Services.Models
{
    /// <summary>
    /// Helper class that describes values returned 
    /// by Google's OAuth server in exchange of authorization 
    /// key.
    /// </summary>
    public class GoogleOauthResponse
    {
        [JsonProperty(PropertyName = "access_token")]
        public string AccessToken { get; set; }

        [JsonProperty(PropertyName = "expires_in")]
        public string ExpiresIn { get; set; }

        [JsonProperty(PropertyName = "scope")]
        public string Scope { get; set; }

        [JsonProperty(PropertyName = "token_type")]
        public string TokenType { get; set; }

        [JsonProperty(PropertyName = "id_token")]
        public string IdToken { get; set; }
    }
}
