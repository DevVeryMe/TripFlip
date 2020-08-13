using System.Net;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Hosting;
using NLog;
using TripFlip.WebApi.ExceptionHandlingExtensions.Models;

namespace TripFlip.WebApi.ExceptionHandlingExtensions
{
    public static class ExceptionMiddlewareExtensions
    {
        public static void ConfigureExceptionHandler(this IApplicationBuilder app, IWebHostEnvironment environment)
        {
            var logger = LogManager.GetCurrentClassLogger();

            app.UseExceptionHandler(options =>
            {
                options.Run(async context =>
                {
                    context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                    context.Response.ContentType = "application/json";

                    var exception = context.Features.Get<IExceptionHandlerFeature>();

                    if (exception != null)
                    {
                        logger.Error(exception);

                        var errorDetails = new ErrorDetails
                        {
                            StatusCode = (int) HttpStatusCode.InternalServerError,
                            Message = "Internal server error."
                        };

                        if (environment.IsDevelopment())
                        {
                            errorDetails.DroppedException = exception.Error;
                        }

                        await context.Response.WriteAsync(errorDetails.ToString());
                    }
                });
            });
        }
    }
}
