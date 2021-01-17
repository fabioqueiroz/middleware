using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Middleware.Client.Interfaces
{
    public interface IMiddlewareService
    {
        Task Run();
    }
}
