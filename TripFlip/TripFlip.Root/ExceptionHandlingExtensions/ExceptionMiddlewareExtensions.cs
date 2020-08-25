using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Hosting;
using NLog;
using System;
using System.Net;
using TripFlip.Root.ExceptionHandlingExtensions.Models;

namespace TripFlip.Root.ExceptionHandlingExtensions
{
    public static class ExceptionMiddlewareExtensions
    {
        /// <summary>
        /// Configures logging and handling exceptions.
        /// </summary>
        public static void ConfigureExceptionHandler(this IApplicationBuilder applicationBuilder,
            IWebHostEnvironment environment)
        {
            var logger = LogManager.GetCurrentClassLogger();

            applicationBuilder.UseExceptionHandler(options =>
            {
                options.Run(async context =>
                {
                    context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                    context.Response.ContentType = "application/json";

                    var exception = context.Features.Get<IExceptionHandlerFeature>();

                    if (exception != null)
                    {
                        logger.Error(exception);

                        if (exception.Error is ArgumentException)
                        {
                            context.Response.StatusCode = (int) HttpStatusCode.NotFound;
                        }

                        var errorDetails = new ErrorDetails
                        {
                            StatusCode = (int) context.Response.StatusCode,
                            Message = exception.Error.Message
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
