using Microsoft.AspNetCore.Builder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace Middleware.Api
{
    public static class RequestManager
    {
        public static void RequestIdentifier(this IApplicationBuilder app, string requestType, Uri requestTargetUri, string sourceRequest, 
            HttpMethod methodType, object model, string connectionString)
        {

        }
    }
}
