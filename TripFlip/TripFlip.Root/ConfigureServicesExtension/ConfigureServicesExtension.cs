using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using TripFlip.Services;
using TripFlip.Services.Interfaces;
using TripFlip.DataAccess;

namespace TripFlip.Root.ConfigureServicesExtension
{
    public static class ConfigureServicesExtension
    {
        public static void ConfigureServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddTransient<IGreetingService, GreetingService>();

            string defaultConnectionStringName = "DefaultConnection";
            services.AddDbContext<FlipTripDbContext>(options =>
                options.UseSqlServer(
                    ConfigurationExtensions.GetConnectionString(
                        configuration, 
                        defaultConnectionStringName
                    )
                )
            );
        }
    }
}
