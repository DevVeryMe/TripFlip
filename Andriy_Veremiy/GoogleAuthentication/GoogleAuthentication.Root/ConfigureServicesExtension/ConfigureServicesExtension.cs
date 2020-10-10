using GoogleAuthentication.DataAccess;
using GoogleAuthentication.Services;
using GoogleAuthentication.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace GoogleAuthentication.Root.ConfigureServicesExtension
{
    public static class ConfigureServicesExtension
    {
        public static void ConfigureServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.ConfigureMapper();

            services.BindJwtConfiguration(configuration);

            services.AddTransient<IUserService, UserService>();

            services.AddDbContext<GoogleAuthenticationDbContext>(options =>
                options.UseSqlServer(
                    configuration.GetConnectionString(Constants.DefaultConnectionStringName
                    )
                )
            );
        }
    }
}
