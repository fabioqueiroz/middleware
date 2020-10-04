using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace Middleware.Api
{
    public static class RequestManager
    {
        public static IApplicationBuilder IoTListenerMiddlewareExtension(this IApplicationBuilder builder, string requestType, Uri requestTargetUri, string sourceRequest, 
            HttpMethod methodType, object model, string connectionString)
        {
            return builder.UseMiddleware<IoTListenerMiddleware>();
        }
    }
}
