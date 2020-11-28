using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Middleware.Data.Access;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace Middleware.Api
{
    public static class IoTListenerExtension
    {
        public static IApplicationBuilder UseIoTListenerMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<IoTListenerMiddleware>();
        }

        //public static IApplicationBuilder UseIoTListenerMiddleware<T>(this IApplicationBuilder builder) where T : class
        //{
        //    Context<T> context;

        //    using (var serviceScope = builder.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope())
        //    {
        //        context = serviceScope.ServiceProvider.GetRequiredService<Context<T>>();

        //        context.Database.EnsureCreated();
        //    }

        //    return builder.UseMiddleware<IoTListenerMiddleware>();
        //}
    }
}
