using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System;
using System.IO;
using System.Reflection;
using System.Text;
using Hangfire;
using Hangfire.SqlServer;
using TripFlip.DataAccess;
using TripFlip.Root.ConfigureServicesExtension;
using TripFlip.Root.ExceptionHandlingExtensions;
using TripFlip.Services.Interfaces;

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

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters()
                    {
                        ValidateIssuer = true,
                        ValidIssuer = Configuration["Jwt:Issuer"],

                        ValidateAudience = true,
                        ValidAudience = Configuration["Jwt:Audience"],

                        ValidateLifetime = true,

                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(
                            Encoding.ASCII.GetBytes(Configuration["Jwt:SecretKey"]))
                    };
                });

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

                options.AddSecurityDefinition("OAuth2", new OpenApiSecurityScheme
                {
                    Type = SecuritySchemeType.OAuth2,

                    Flows = new OpenApiOAuthFlows
                    {
                        AuthorizationCode = new OpenApiOAuthFlow
                        {
                            AuthorizationUrl = new Uri(Configuration["GoogleApi:AuthorizationUri"]),
                            TokenUrl = new Uri(Configuration["GoogleApi:RedirectUri"])
                        }
                    }
                });
            });

            services.AddHangfire(configuration => configuration
                .SetDataCompatibilityLevel(CompatibilityLevel.Version_170)
                .UseSimpleAssemblyNameTypeSerializer()
                .UseRecommendedSerializerSettings()
                .UseSqlServerStorage(Configuration.GetConnectionString("HangfireTasksStorage"), new SqlServerStorageOptions
                {
                    CommandBatchMaxTimeout = TimeSpan.FromMinutes(5),
                    SlidingInvisibilityTimeout = TimeSpan.FromMinutes(5),
                    QueuePollInterval = TimeSpan.FromMilliseconds(100),
                    UseRecommendedIsolationLevel = true,
                    DisableGlobalLocks = true
                }));

            services.ConfigureServices(Configuration);

            services.AddHttpContextAccessor();
        }

        public void Configure(IApplicationBuilder applicationBuilder, IWebHostEnvironment environment, 
            TripFlipDbContext tripFlipDbContext)
        {
            applicationBuilder.ConfigureExceptionHandler(environment);

            applicationBuilder.UseRouting();

            applicationBuilder.UseAuthentication();
            applicationBuilder.UseAuthorization();

            applicationBuilder.UseHangfireDashboard();
            RecurringJob.AddOrUpdate<IScheduledTaskService>(
                scheduledTask => scheduledTask.GreetBirthdayUsersAsync(),
                Cron.Daily(hour: 8));
            RecurringJob.AddOrUpdate<IScheduledTaskService>(
                scheduledTask => scheduledTask.SendUserStatisticAsync(),
                Cron.Monthly());

            applicationBuilder.UseHangfireServer();

            applicationBuilder.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            var swaggerEndpointUrl = Configuration.GetSection("OpenApiInfo")["url"];
            var swaggerApiVersion = Configuration.GetSection("OpenApiInfo")["version"];

            applicationBuilder.UseSwagger();
            applicationBuilder.UseSwaggerUI(options =>
            {
                options.SwaggerEndpoint(swaggerEndpointUrl, swaggerApiVersion);
                options.OAuthClientId(Configuration["GoogleApi:ClientId"]);
                options.OAuthClientSecret(Configuration["GoogleApi:ClientSecret"]);
                options.OAuthAppName("TripFlip");
                options.OAuthScopeSeparator(" ");
                options.OAuthUsePkce();
            });

            tripFlipDbContext.Database.Migrate();
        }
    }
}
