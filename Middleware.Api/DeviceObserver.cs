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
using static Middleware.Data.Access.HttpMiddlewareInterceptor;

namespace Middleware.Api
{
    public class DeviceObserver : IObserver<KeyValuePair<string, object>>
    {
        private readonly IOptions<HttpMiddlewareInterceptor> _options;
        private readonly IDeviceRepository _deviceRepository;
        private readonly IServiceProvider _serviceProvider;
        private IServiceScope _scope;

        public DeviceObserver(IOptions<HttpMiddlewareInterceptor> options, IServiceProvider serviceProvider)
        {
            _options = options;
            _serviceProvider = serviceProvider;
            _scope = _serviceProvider.CreateScope();
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
                //var requestMethod = httpContext.Request.Method;
                //var url = httpContext.Request.Host.Value + httpContext.Request.Path.Value;

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

                        Func<HttpContext, DeviceModel, Device> DeviceMapper = DelegateMappers.MapToSpecificDevice;
                        
                        await _deviceRepository.AddAsync(DeviceMapper(httpContext, device));
                    }
                }
            }
        }
    }
}
