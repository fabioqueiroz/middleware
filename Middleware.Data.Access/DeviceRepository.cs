using Microsoft.Extensions.Configuration;
using Middleware.Api;
using Middleware.Data.Access.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Middleware.Data.Access
{
    public class DeviceRepository : BaseRepository<Device>, IDeviceRepository, IDisposable
    {
        public DeviceRepository(Context context) : base(context)
        {

        }
 
        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
