namespace TripFlip.Services.Configurations
{
    public class GoogleApiConfiguration
    {
        public string ClientId { get; set; }

        public string ClientSecret { get; set; }

        public string AuthorizationUri { get; set; }

        public string RedirectUri { get; set; }

        public string GoogleOpenIdConfigurationUri { get; set; }
    }
}
