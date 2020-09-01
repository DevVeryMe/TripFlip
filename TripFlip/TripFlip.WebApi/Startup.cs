using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.IO;
using System.Reflection;
using Microsoft.OpenApi.Models;
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

                // Set JWT usage.
                options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    In = ParameterLocation.Header,
                    Description = "Please insert JWT with Bearer into field",
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey
                });

                options.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            }
                        },
                        new string[] { }
                    }
                });
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
