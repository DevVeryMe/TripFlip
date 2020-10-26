using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TripFlip.DataAccess;
using TripFlip.Services;
using TripFlip.Services.Helpers;
using TripFlip.Services.Interfaces;

namespace TripFlip.Root.ConfigureServicesExtension
{
    public static class ConfigureServicesExtension
    {
        public static void ConfigureServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.ConfigureMapper();

            services.BindJwtConfiguration(configuration);

            services.BindSendGridConfiguration(configuration);

            services.BindGoogleApiConfiguration(configuration);

            services.AddHttpClient();

            services.AddTransient<ITaskService, TaskService>();

            services.AddTransient<ITripService, TripService>();

            services.AddTransient<ITaskListService, TaskListService>();

            services.AddTransient<IItemService, ItemService>();

            services.AddTransient<IRouteService, RouteService>();

            services.AddTransient<IItemListService, ItemListService>();

            services.AddTransient<IUserService, UserService>();

            services.AddTransient<IGoogleApiUserService, GoogleApiUserService>();

            services.AddTransient<IMailService, MailService>();

            services.AddTransient<IStatisticService, StatisticService>();

            services.AddSingleton<ICurrentUserService, CurrentUserService>();

            services.AddDbContext<TripFlipDbContext>(options =>
                options.UseSqlServer(
                    ConfigurationExtensions.GetConnectionString(
                        configuration, 
                        Constants.DefaultConnectionStringName
                    )
                )
            );
        }
    }
}
