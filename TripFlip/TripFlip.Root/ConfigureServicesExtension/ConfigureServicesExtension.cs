using AutoMapper;
using Microsoft.Extensions.DependencyInjection;
using TripFlip.Root.MappingProfiles;
using TripFlip.Services;
using TripFlip.Services.Interfaces;

namespace TripFlip.Root.ConfigureServicesExtension
{
    public static class ConfigureServicesExtension
    {
        public static void ConfigureServices(this IServiceCollection services)
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
        }
    }
}
