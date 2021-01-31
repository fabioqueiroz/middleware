using System;
using System.Collections.Generic;
using System.Text;

namespace Middleware.Data.Access
{
    public class HttpMiddlewareInterceptor
    {
        public string ConnectionString { get; set; }
        public Interceptor Interceptors { get; set; }
    }

    public class Interceptor
    {
        public string Method { get; set; }
        public string Endpoint { get; set; }
    }

}
