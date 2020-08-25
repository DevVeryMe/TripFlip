using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.IO;
using System.Reflection;
using TripFlip.DataAccess;
using TripFlip.Root.ConfigureServicesExtension;
using TripFlip.Root.ExceptionHandlingExtensions;

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

            services.AddSwaggerGen(options =>
            {
                // Set the comments path for the Swagger JSON and UI.
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                options.IncludeXmlComments(xmlPath);
            });

            services.ConfigureServices(Configuration);
        }

        public void Configure(IApplicationBuilder applicationBuilder, IWebHostEnvironment environment, 
            TripFlipDbContext tripFlipDbContext)
        {
            applicationBuilder.ConfigureExceptionHandler(environment);

            applicationBuilder.UseRouting();

            applicationBuilder.UseAuthorization();

            applicationBuilder.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            string swaggerEndpointUrl = Configuration.GetSection("OpenApiInfo")["url"];
            string swaggerApiVersion = Configuration.GetSection("OpenApiInfo")["version"];
            applicationBuilder.UseSwagger();
            applicationBuilder.UseSwaggerUI(
                options => options.SwaggerEndpoint(swaggerEndpointUrl, swaggerApiVersion)
            );

            tripFlipDbContext.Database.Migrate();
        }
    }
}
