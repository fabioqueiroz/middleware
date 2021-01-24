using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Middleware.Api.Models
{
    public class DeviceModel : BaseDevice
    {
        public int Id { get; set; }
        public byte[] Payload { get; set; }
        public double Longitude { get; set; }
        public double Latitude { get; set; }
    }
}
