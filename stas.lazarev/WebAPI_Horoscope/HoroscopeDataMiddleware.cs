using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace WebAPI_Horoscope
{
    /// <summary>
    /// A middleware that is responsible for using HoroscopeData service
    /// </summary>
    public class HoroscopeDataMiddleware : IMiddleware
    {
        HoroscopeDataService _horoscopeData;

        public HoroscopeDataMiddleware(HoroscopeDataService horoscopeData)
        {
            _horoscopeData = horoscopeData;
        }

        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            if (_horoscopeData.Data == null)
            {
                await context.Response.WriteAsync("Error: Horoscope data collection has failed to initialize");
            }
            else
            {
                await next.Invoke(context);
            }
        }
    }
}
