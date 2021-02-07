using Middleware.Business.Commons;
using System;
using System.Collections.Generic;
using System.Text;

namespace Middleware.Business.Models
{
    /// <summary>
    /// This class has all the properties of all types
    /// of possible devices that may come via a request
    /// </summary>
    public class DeviceBusinessModel
    {
        public int Id { get; set; }
        public byte[] Payload { get; set; }
        public double Longitude { get; set; }
        public double Latitude { get; set; }
        public string RequestOrigin { get; set; }
        public string RequestMethod { get; set; }
        public string DelegateName { get; set; }
        public DateTime DateReceived { get; set; }
        public TypeEnum Type { get; set; }
    }
}
