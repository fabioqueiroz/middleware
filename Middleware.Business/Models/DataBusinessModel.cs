using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Middleware.Business.Models
{
    public class DataBusinessModel
    {
        [JsonProperty("data")]
        public List<DeviceBusinessModel> DeviceBusinessModels { get; set; }
    }
}
