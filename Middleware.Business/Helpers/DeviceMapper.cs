using Microsoft.AspNetCore.Http;
using Middleware.Business.Commons;
using Middleware.Business.Models;
using Middleware.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace Middleware.Business.Helpers
{
    public class DeviceMapper
    {
        public static Device MapToSpecificDevice(HttpContext httpContext, DeviceBusinessModel deviceModel)
        {
            deviceModel.DateReceived = DateTime.UtcNow;
            deviceModel.RequestMethod = httpContext.Request.Method;
            deviceModel.RequestOrigin = GetRequestOrigin(httpContext);

            var deviceResult = new Device();

            if (deviceModel.RequestMethod.Equals(BusinessConstants.RequestType.Post))
            {
                switch (deviceModel.DelegateName)
                {
                    case BusinessConstants.DelegateNames.PostmanDelegate:
                        deviceResult = MapToPostmanDevice(deviceModel);
                        break;
                    case BusinessConstants.DelegateNames.HostDelegate:
                        deviceResult = MapToHostDevice(deviceModel);
                        break;
                }
            }

            return deviceResult;
        }
        private static string GetRequestOrigin(HttpContext httpContext)
        {
            var requestOrigin = string.Empty;

            if (httpContext.Request.Headers.ContainsKey(BusinessConstants.RequestOrigin.PostmanToken))
            {
                requestOrigin = BusinessConstants.RequestOrigin.PostmanToken;
            }
            else if (httpContext.Request.Headers.ContainsKey(BusinessConstants.RequestOrigin.HostToken))
            {
                requestOrigin = BusinessConstants.RequestOrigin.HostToken;
            }

            return requestOrigin;
        }

        public static Device MapToPostmanDevice(DeviceBusinessModel DeviceBusinessModel)
        {
            var deviceToReturn = MapIncomingModelToDevice(DeviceBusinessModel);
            // perform specifc actions/bindings for this type of device
            deviceToReturn.Type = (int)TypeEnum.Postman;
            return deviceToReturn;
        }

        public static Device MapToHostDevice(DeviceBusinessModel DeviceBusinessModel)
        {
            var deviceToReturn = MapIncomingModelToDevice(DeviceBusinessModel);
            // perform specifc actions/bindings for this type of device
            deviceToReturn.Type = (int)TypeEnum.Host;
            return deviceToReturn;
        }

        private static Device MapIncomingModelToDevice(DeviceBusinessModel DeviceBusinessModel)
        {
            return new Device
            {
                Latitude = DeviceBusinessModel.Location.Latitude,
                Longitude = DeviceBusinessModel.Location.Longitude,
                Payload = DeviceBusinessModel.Payload,
                DateReceived = DeviceBusinessModel.DateReceived,
                RequestOrigin = DeviceBusinessModel.RequestOrigin
            };
        }
    }
}
