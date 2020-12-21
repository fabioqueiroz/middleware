﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Middleware.Api.Models
{
    public class RequestModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Code { get; set; }
        public byte[] Payload { get; set; }
        public double Longitude { get; set; }
        public double Latitude { get; set; }
    }
}
