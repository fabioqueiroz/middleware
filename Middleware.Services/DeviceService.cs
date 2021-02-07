using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Middleware.Business.Commons;
using Middleware.Business.Helpers;
using Middleware.Business.Models;
using Middleware.Data;
using Middleware.Data.Access.Interfaces;
using Middleware.Services.Interfaces;
using System;
using System.Threading.Tasks;

namespace Middleware.Services
{
    public class DeviceService : IDeviceService
    {
        private readonly IDeviceRepository _deviceRepository;
        private readonly IServiceProvider _serviceProvider;
        private IServiceScope _scope;

        public DeviceService(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
            _scope = _serviceProvider.CreateScope();
            _deviceRepository = _scope.ServiceProvider.GetRequiredService<IDeviceRepository>();
        }

        public async void AddNewDevice(HttpContext httpContext, DeviceBusinessModel deviceBusinessModel)
        {
            var mappedDevice = DeviceMapper.MapToSpecificDevice(httpContext, deviceBusinessModel);

            switch (mappedDevice.Type)
            {
                case (int)TypeEnum.Postman:
                    // do something
                    break;
                case (int)TypeEnum.Host:
                    // do something else
                    break;
                case (int)TypeEnum.Sigfox:
                    // do something else
                    break;
            }

            await _deviceRepository.AddAsync(mappedDevice);
        }
    }
}
