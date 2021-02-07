
using Microsoft.AspNetCore.Http;
using Middleware.Business;
using Middleware.Business.Models;
using Middleware.Data;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Middleware.Services.Interfaces
{
    public interface IDeviceService
    {
        void AddNewDevice(HttpContext httpContext, DeviceBusinessModel deviceBusinessModel);
    }
}
