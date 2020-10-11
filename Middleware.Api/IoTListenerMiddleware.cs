using Microsoft.AspNetCore.Http;
using Middleware.Api.Models;
using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Dynamic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Middleware.Api
{
    public class IoTListenerMiddleware//<T> where T : class
    {
        private readonly RequestDelegate _next;

        public IoTListenerMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            // 1) Detect the incoming method
            var requestMethod = context.Request.Method;

            // 2) detect the Url
            var url = context.Request.Host.Value + context.Request.Path.Value;

            // 3) get the body from this request
            var requestBody = context.Request.HttpContext.Request;
            requestBody.EnableBuffering();

            var bodyString = string.Empty;
            using (var reader = new StreamReader(requestBody.Body, Encoding.UTF8, true, 1024, true))
            {
                bodyString = await reader.ReadToEndAsync();
            }

            requestBody.Body.Position = 0;

            var deserializedBody = JsonConvert.DeserializeObject<object>(bodyString);

            // 4) map the body of the response
            // this is supposed to split all the properties and values in the deserialized body
            var response = new ExpandoObject(); 

            // next step: map the expando obj using reflection to a concrete class
            // https://stackoverflow.com/questions/3862226/how-to-dynamically-create-a-class

            // 5) persist in to the db

            await _next(context);
        }

        //private IDictionary DataDictionary(string bodyString, Regex regex)
        //{
        //    //var regex = new Regex(@"\b(?<word>\w+)"); // @"\b[A-Za-z|0-9]\w+";
        //    //var result = DataDictionary(bodyString, regex);

        //    var matches = regex.Matches(bodyString);
        //    var result = new Dictionary<string, object>();

        //    for (int i = 0; i < matches.Count; i++)
        //    {
        //        if (i % 2 == 0)
        //        {
        //            result.Add(matches[i].Value, matches[i + 1].Value);
        //        }
        //    }

        //    return result;
        //}
    }
}
