using Middleware.Api.Models;
using Middleware.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Middleware.Api
{
    public class CustomMappers
    {
        public static Device MapToDeviceDb(string requestOrigin, DeviceModel device)
        {
            return new Device
            {
                Latitude = device.Latitude,
                Longitude = device.Longitude,
                Payload = device.Payload,
                DateReceived = device.DateReceived,
                RequestOrigin = requestOrigin
            };
        }
    }
}
