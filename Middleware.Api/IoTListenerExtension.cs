using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
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
    }
}
