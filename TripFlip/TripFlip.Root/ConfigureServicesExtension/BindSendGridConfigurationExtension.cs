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
            var mailServiceConfiguration = new MailServiceConfiguration();

            configuration.GetSection(Constants.MailServiceSection).Bind(mailServiceConfiguration);
            services.AddSingleton(mailServiceConfiguration);
        }
    }
}
