using GoogleAuthentication.Services.Configurations;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;

namespace GoogleAuthentication.Root.ConfigureServicesExtension
{
    public static class BindGoogleConfigurationExtension
    {
        public static void BindGoogleConfiguration(
            this IServiceCollection services,
            IConfiguration configuration)
        {
            var googleConfiguration = new GoogleAuthorizationConfiguration();

            configuration.GetSection(Constants.GoogleAuthorizationSection).Bind(googleConfiguration);
            services.AddSingleton(googleConfiguration);
        }
    }
}
