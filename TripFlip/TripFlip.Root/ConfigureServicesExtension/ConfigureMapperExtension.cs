using AutoMapper;
using Microsoft.Extensions.DependencyInjection;
using TripFlip.Root.MappingProfiles;

namespace TripFlip.Root.ConfigureServicesExtension
{
    public static class ConfigureMapperExtension
    {
        public static void ConfigureMapper(this IServiceCollection services)
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
        }
    }
}
