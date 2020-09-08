using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TripFlip.Services.Configurations;

namespace TripFlip.Root.ConfigureServicesExtension
{
    public static class BindJwtConfigurationExtension
    {
        public static void BindJwtConfiguration(
            this IServiceCollection services,
            IConfiguration configuration)
        {
            var jwtConfiguration = new JwtConfiguration();

            configuration.GetSection(Constants.JwtSection).Bind(jwtConfiguration);
            services.AddSingleton(jwtConfiguration);
        }
    }
}
