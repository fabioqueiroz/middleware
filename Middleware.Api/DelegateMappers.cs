using Microsoft.AspNetCore.Http;
using Middleware.Api.Commons;
using Middleware.Api.Models;
using Middleware.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Middleware.Api
{
    public class DelegateMappers
    {
        public static Device MapToSpecificDevice(HttpContext httpContext, DeviceModel deviceModel)
        {
            deviceModel.DateReceived = DateTime.UtcNow;
            deviceModel.RequestMethod = httpContext.Request.Method;
            deviceModel.RequestOrigin = GetRequestOrigin(httpContext);

            var DeviceResult = new Device();

            if (deviceModel.RequestMethod.Equals(SystemConstants.RequestType.Post))
            {
                switch (deviceModel.DelegateName)
                {
                    case SystemConstants.DelegateNames.PostmanDelegate:
                        DeviceResult = MapToPostmanDevice(deviceModel);
                        break;
                    case SystemConstants.DelegateNames.HostDelegate:
                        DeviceResult = MapToHostDevice(deviceModel);
                        break;
                } 
            }

            return DeviceResult;
        }

        private static string GetRequestOrigin(HttpContext httpContext)
        {
            var requestOrigin = string.Empty;

            if (httpContext.Request.Headers.ContainsKey(SystemConstants.RequestOrigin.PostmanToken))
            {
                requestOrigin = SystemConstants.RequestOrigin.PostmanToken;
            }
            else if (httpContext.Request.Headers.ContainsKey(SystemConstants.RequestOrigin.HostToken))
            {
                requestOrigin = SystemConstants.RequestOrigin.HostToken;
            }

            return requestOrigin;
        }

        public static Device MapToPostmanDevice(DeviceModel deviceModel)
        {
            var deviceToReturn = MapIncomingModelToDevice(deviceModel);
            // perform specifc actions/bindings for this type of device
            return deviceToReturn;
        }

        public static Device MapToHostDevice(DeviceModel deviceModel)
        {           
            var deviceToReturn = MapIncomingModelToDevice(deviceModel);
            // perform specifc actions/bindings for this type of device
            return deviceToReturn;
        }

        private static Device MapIncomingModelToDevice(DeviceModel deviceModel)
        {
            return new Device
            {
                Latitude = deviceModel.Latitude,
                Longitude = deviceModel.Longitude,
                Payload = deviceModel.Payload,
                DateReceived = deviceModel.DateReceived.ToString(),
                RequestOrigin = deviceModel.RequestOrigin
            };
        }
    }
}
