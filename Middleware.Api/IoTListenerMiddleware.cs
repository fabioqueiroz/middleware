using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace Middleware.Api
{
    public class IoTListenerMiddleware
    {
        private readonly RequestDelegate _next;

        public IoTListenerMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            var cultureQuery = context.Request.Query["culture"];
            if (!string.IsNullOrWhiteSpace(cultureQuery))
            {
                var culture = new CultureInfo(cultureQuery);

                CultureInfo.CurrentCulture = culture;
                CultureInfo.CurrentUICulture = culture;
            }

            // 1) Detect the incoming method
            // 2) detect the Url
            // 3) get the body from this request
            // 4) map the body of the response
            // 5) persist in to the db

            // Call the next delegate/middleware in the pipeline
            await _next(context);
        }
    }
}
