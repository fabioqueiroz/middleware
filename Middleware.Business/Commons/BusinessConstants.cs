using System;
using System.Collections.Generic;
using System.Text;

namespace Middleware.Business.Commons
{
    public class BusinessConstants
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
