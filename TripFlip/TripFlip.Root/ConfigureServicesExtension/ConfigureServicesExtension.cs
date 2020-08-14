using Microsoft.Extensions.DependencyInjection;
using TripFlip.Services;
using TripFlip.Services.Interfaces;

namespace TripFlip.Root.ConfigureServicesExtension
{
    public static class ConfigureServicesExtension
    {
        public static void ConfigureServices(this IServiceCollection services)
        {
            services.AddTransient<IGreetingService, GreetingService>();
        }
    }
}
