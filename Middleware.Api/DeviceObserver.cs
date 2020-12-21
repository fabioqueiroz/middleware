using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Middleware.Data;
using Middleware.Data.Access;
using Middleware.Data.Access.Interfaces;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Middleware.Api
{
    public class DeviceObserver : IObserver<KeyValuePair<string, object>>
    {
        private readonly IOptions<HttpMiddlewareInterceptor> _options;
        private readonly IDeviceRepository _deviceRepository;
        //private readonly IApplicationBuilder _applicationBuilder;
        //private readonly IServiceProvider _serviceProvider;
        public DeviceObserver(IOptions<HttpMiddlewareInterceptor> options)//, IDeviceRepository deviceRepository) // IServiceProvider serviceProvider
        {
            _options = options;
            //_deviceRepository = deviceRepository;
            //_serviceProvider = serviceProvider;
            //_applicationBuilder = new ApplicationBuilder(_serviceProvider);
        }
        public void OnCompleted()
        {
            throw new NotImplementedException();
        }

        public void OnError(Exception error)
        {
            throw new NotImplementedException();
        }

        public async void OnNext(KeyValuePair<string, object> value)
        {
            if (value.Key == "Microsoft.AspNetCore.Hosting.HttpRequestIn.Start")
            {
                var httpContext = value.Value.GetType().GetProperty("HttpContext")?.GetValue(value.Value) as HttpContext;
                if (httpContext != null)
                {
                    var requestBody = httpContext.Request.HttpContext.Request;
                    if (requestBody.ContentLength > 0)
                    {
                        requestBody.EnableBuffering();

                        var bodyString = string.Empty;
                        using (var reader = new StreamReader(requestBody.Body, Encoding.UTF8, true, 1024, true))
                        {
                            bodyString = await reader.ReadToEndAsync();
                        }

                        requestBody.Body.Position = 0;

                        var device = JsonConvert.DeserializeObject<Device>(bodyString);
                        device.DateReceived = DateTime.UtcNow;

                        var deviceData = new DeviceData
                        {
                           Latitude = device.Latitude,
                           Longitude = device.Longitude,
                           Payload = device.Payload,
                           DateReceived = device.DateReceived
                        };

                        //_deviceRepository.Add(deviceData); // DI error
                        //PersistInDb<DeviceData>(deviceData);
                    } 
                }
            }
        }

        //private void PersistInDb<T>(object dynamicObj) where T : class
        //{
        //    try
        //    {
        //        Context<T> context;

        //        using var serviceScope = _applicationBuilder.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope();

        //        context = serviceScope.ServiceProvider.GetRequiredService<Context<T>>();

        //        context.Database.EnsureCreated();

        //        try
        //        {
        //            context.Add((DeviceData)dynamicObj);
        //        }
        //        catch (Exception ex)
        //        {
        //            Console.WriteLine(ex.Message);
        //        }
        //    }
        //    catch (Exception ex)
        //    {

        //        Console.WriteLine(ex.Message);
        //    }
        //}
    }
}
