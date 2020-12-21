using System;
using System.Collections.Generic;
using System.Text;

namespace Middleware.Data
{
    public class DeviceData
    {
        public int Id { get; set; }
        public byte[] Payload { get; set; }
        public double Longitude { get; set; }
        public double Latitude { get; set; }
        public DateTime DateReceived { get; set; }
    }
}
