using AutoMapper;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using TripFlip.Root.MappingProfiles;
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

            var mappingConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile<EntityToDTO>();
                mc.AddProfile<EntityFromDTO>();
                mc.AddProfile<ViewModelToDTO>();
                mc.AddProfile<ViewModelFromDTO>();
            });
            IMapper mapper = mappingConfig.CreateMapper();
            services.AddSingleton(mapper);

            
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
