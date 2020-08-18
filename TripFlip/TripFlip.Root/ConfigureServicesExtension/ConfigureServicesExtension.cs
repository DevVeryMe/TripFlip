using AutoMapper;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using TripFlip.Root.MappingProfiles;
using TripFlip.Services;
using TripFlip.Services.Interfaces;
using TripFlip.DataAccess;
using TripFlip.Services.Interfaces.TripInterfaces;

namespace TripFlip.Root.ConfigureServicesExtension
{
    public static class ConfigureServicesExtension
    {
        public static void ConfigureServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.ConfigureMapper();
            services.AddTransient<IGreetingService, GreetingService>();
            services.AddTransient<ITripService, TripService>();

            services.AddDbContext<FlipTripDbContext>(options =>
                options.UseSqlServer(
                    ConfigurationExtensions.GetConnectionString(
                        configuration, 
                        Constants.defaultConnectionStringName
                    )
                )
            );
        }
    }
}
