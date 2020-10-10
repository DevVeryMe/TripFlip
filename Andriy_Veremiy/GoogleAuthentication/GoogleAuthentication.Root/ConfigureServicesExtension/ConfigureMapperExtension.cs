using AutoMapper;
using GoogleAuthentication.Root.MappingProfiles;
using Microsoft.Extensions.DependencyInjection;

namespace GoogleAuthentication.Root.ConfigureServicesExtension
{
    public static class ConfigureMapperExtension
    {
        public static void ConfigureMapper(this IServiceCollection services)
        {
            var mappingConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile<EntityToDto>();
                mc.AddProfile<ViewModelFromDto>();
            });

            var mapper = mappingConfig.CreateMapper();
            services.AddSingleton(mapper);
        }
    }
}
