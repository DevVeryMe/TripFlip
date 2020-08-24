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

            services.AddTransient<ITaskService, TaskService>();

            services.AddTransient<ITripService, TripService>();

            services.AddTransient<ITaskListService, TaskListService>();

            services.AddTransient<IItemService, ItemService>();

            services.AddTransient<IRouteService, RouteService>();

            services.AddTransient<IITemListService, ItemListService>();

            services.AddDbContext<TripFlipDbContext>(options =>
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
