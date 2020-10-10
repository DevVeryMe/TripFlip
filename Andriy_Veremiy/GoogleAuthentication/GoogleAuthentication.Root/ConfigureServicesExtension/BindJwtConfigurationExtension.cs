using GoogleAuthentication.Services.Configurations;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;

namespace GoogleAuthentication.Root.ConfigureServicesExtension
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
