using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Internal;
using Microsoft.Extensions.DependencyInjection;
using Middleware.Api.Models;
using Middleware.Data;
using Middleware.Data.Access;
using Middleware.Data.Access.Interfaces;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Dynamic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.Loader;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Middleware.Api
{
    public class IoTListenerMiddleware//<T> where T : class
    {
        private readonly RequestDelegate _next;
        private readonly IApplicationBuilder _applicationBuilder;
        private readonly IServiceProvider _serviceProvider;
        //private readonly IDeviceRepository _deviceRepository;

        public IoTListenerMiddleware(RequestDelegate next, IServiceProvider serviceProvider)//, IDeviceRepository deviceRepository)
        {
            _next = next;
            _serviceProvider = serviceProvider;
            _applicationBuilder = new ApplicationBuilder(_serviceProvider);
            //_deviceRepository = deviceRepository;
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

            var deserializedBody = JsonConvert.DeserializeObject<JObject>(bodyString);

            // 4) map the body of the response
            var dynamicDictionary = new Dictionary<string, object>();

            if (deserializedBody != null && deserializedBody.HasValues)
            {
                dynamicDictionary = deserializedBody.ToObject<Dictionary<string, object>>();

                // Create a class at runtime
                dynamic builder = DynamicTypeBuilder.CreateNewObject(dynamicDictionary);
                Type dynamicType = builder.GetType();
                var rtProperties = dynamicType.GetRuntimeProperties().ToList();
                var newObj = Activator.CreateInstance(dynamicType);
                AssignDynamicValues(newObj, rtProperties, dynamicDictionary);
              
                // 5) persist into the db - **** UNABLE TO CAST RUNTTIME GENERATED CLASS, EF DOES NOT RECOGNISE THE CLASS AS BEING THE SAME ****
                //PersistInDb<Middleware.Api.Device>(newObj);
            }

            // moved logic to the observer to persist the device

            await _next(context);
        }

        private void PersistInDb<T>(object dynamicObj) where T : class
        {
            try
            {
                Context<T> context;

                using var serviceScope = _applicationBuilder.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope();

                context = serviceScope.ServiceProvider.GetRequiredService<Context<T>>();

                context.Database.EnsureCreated();

                try
                {
                    context.Add<Middleware.Api.Device>((Middleware.Api.Device)dynamicObj);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }               
            }
            catch (Exception ex)
            {

                Console.WriteLine(ex.Message);
            }
        }

        private void AssignDynamicValues(object objToMap, List<PropertyInfo> runTimeProperties, Dictionary<string, object> deserializedValues)
        {
            foreach (KeyValuePair<string, object> kv in deserializedValues)
            {
                foreach (var property in runTimeProperties)
                {
                    if (property.Name.Equals(kv.Key))
                    {
                        property.SetValue(objToMap, kv.Value);
                    }
                }
            }
        }      
    }
}
