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
            var mappingConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile<EntityToDto>();
                mc.AddProfile<EntityFromDto>();
                mc.AddProfile<ViewModelToDto>();
                mc.AddProfile<ViewModelFromDto>();
            });
            IMapper mapper = mappingConfig.CreateMapper();
            services.AddSingleton(mapper);

            services.AddTransient<ITripService, TripService>();
            services.AddTransient<IRouteService, RouteService>();

            
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
