using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TripFlip.Root.ExceptionHandlingExtensions;
using TripFlip.Root.ConfigureServicesExtension;

namespace TripFlip.WebApi
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();

            services.AddSwaggerGen();

            services.ConfigureServices(Configuration);
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.ConfigureExceptionHandler(env);

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            string swaggerEndpointUrl = Configuration.GetSection("OpenApiInfo")["url"];
            string swaggerApiVersion = Configuration.GetSection("OpenApiInfo")["version"];
            app.UseSwagger();
            app.UseSwaggerUI(
                options => options.SwaggerEndpoint(swaggerEndpointUrl, swaggerApiVersion)
            );
        }
    }
}
