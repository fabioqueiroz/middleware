using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Middleware.Api.Commons
{
    public class SystemConstants
    {
        public class RequestOrigin 
        {
            public const string PostmanToken = "Postman-Token";
            public const string HostToken = "Host";
        }

        public sealed class RequestType
        {
            public const string Get = "GET";
            public const string Post = "POST";
            public const string Put = "PUT";
            public const string Delete = "DELETE";
        }

        public class DelegateNames
        {
            public const string PostmanDelegate = "PostmanDelegate";
            public const string HostDelegate = "HostDelegate";
        }
    }
}
