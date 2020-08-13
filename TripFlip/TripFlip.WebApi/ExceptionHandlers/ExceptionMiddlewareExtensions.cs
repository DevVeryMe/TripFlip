using System.Net;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Hosting;
using NLog;
using TripFlip.WebApi.ExceptionFilters.Models;

namespace TripFlip.WebApi.ExceptionFilters
{
    public static class ExceptionMiddlewareExtensions
    {
        public static void ConfigureExceptionHandler(this IApplicationBuilder app, IWebHostEnvironment environment)
        {
            var logger = LogManager.GetCurrentClassLogger();

            app.UseExceptionHandler(appError =>
            {
                appError.Run(async context =>
                {
                    context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                    context.Response.ContentType = "application/json";

                    var contextFeature = context.Features.Get<IExceptionHandlerFeature>();

                    if (contextFeature != null)
                    {
                        logger.Error(contextFeature);

                        var errorDetails = new ErrorDetails
                        {
                            StatusCode = (int) HttpStatusCode.InternalServerError,
                            Message = "Internal server error."
                        };

                        if (environment.IsDevelopment())
                        {
                            errorDetails.DroppedException = contextFeature.Error;
                        }

                        await context.Response.WriteAsync(errorDetails.ToString());
                    }
                });
            });
        }
    }
}
