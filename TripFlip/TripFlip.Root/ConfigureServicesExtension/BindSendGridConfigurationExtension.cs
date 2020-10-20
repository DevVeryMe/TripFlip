using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TripFlip.Services.Configurations;

namespace TripFlip.Root.ConfigureServicesExtension
{
    public static class BindSendGridConfigurationExtension
    {
        public static void BindSendGridConfiguration(
            this IServiceCollection services,
            IConfiguration configuration)
        {
            var sendGridConfiguration = new SendGridConfiguration();

            configuration.GetSection(Constants.SendGridSection).Bind(sendGridConfiguration);
            services.AddSingleton(sendGridConfiguration);
        }
    }
}
