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
using Microsoft.AspNetCore.Mvc.NewtonsoftJson;
using Microsoft.EntityFrameworkCore;
using Middleware.Api.Commons;
using Middleware.Api.Models;

namespace Middleware.Api
{
    public class DeviceObserver : IObserver<KeyValuePair<string, object>>
    {
        private readonly IOptions<HttpMiddlewareInterceptor> _options;
        private readonly IDeviceRepository _deviceRepository;
        private readonly IServiceProvider _serviceProvider;
        private IServiceScope _scope;
        //private DbContext _context;

        public DeviceObserver(IOptions<HttpMiddlewareInterceptor> options, IServiceProvider serviceProvider) // , IDeviceRepository deviceRepository
        {
            _options = options;
            //_deviceRepository = deviceRepository;
            _serviceProvider = serviceProvider;
            _scope = _serviceProvider.CreateScope();
            //_context = _scope.ServiceProvider.GetRequiredService<Context<DeviceData>>();
            _deviceRepository = _scope.ServiceProvider.GetRequiredService<IDeviceRepository>(); // artificial DI
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
                var requestMethod = httpContext.Request.Method;
                var url = httpContext.Request.Host.Value + httpContext.Request.Path.Value;

                //1 - pass delegate to middleware
                //2 - pass a dicctionary with a<ClassType, Delegate>,
                //where the Classtype will be the key and the value will be the delegate

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

                        var device = JsonConvert.DeserializeObject<DeviceModel>(bodyString);
                        device.DateReceived = DateTime.UtcNow;

                        Func<string, DeviceModel, Device> DeviceMapper = CustomMappers.MapToDeviceDb;

                        if (requestMethod.Equals("POST"))
                        {
                            var key = string.Empty;

                            if (httpContext.Request.Headers.ContainsKey("Postman-Token"))
                            {
                                key = SystemConstants.RequestOrigin.POSTMAN_TOKEN;
                            }
                            else if (httpContext.Request.Headers.ContainsKey("Host"))
                            {
                                key = SystemConstants.RequestOrigin.HOST;
                            }

                            await _deviceRepository.AddAsync(DeviceMapper(key, device));
                        }                     
                    }
                }
            }
        }
    }
}
