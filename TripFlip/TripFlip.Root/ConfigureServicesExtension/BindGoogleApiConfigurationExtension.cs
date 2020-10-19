using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TripFlip.Services.Configurations;

namespace TripFlip.Root.ConfigureServicesExtension
{
    public static class BindGoogleApiConfigurationExtension
    {
        public static void BindGoogleApiConfiguration(
            this IServiceCollection services,
            IConfiguration configuration)
        {
            var googleApiConfiguration = new GoogleApiConfiguration();

            configuration.GetSection(Constants.GoogleApiSection).Bind(googleApiConfiguration);

            services.AddSingleton(googleApiConfiguration);
        }
    }
}
