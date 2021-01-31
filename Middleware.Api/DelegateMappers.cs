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
        public static Device MapToSpecificDevice(DeviceModel deviceModel)
        {
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
                DateReceived = deviceModel.DateReceived,
                RequestOrigin = deviceModel.RequestOrigin
            };
        }
    }
}
