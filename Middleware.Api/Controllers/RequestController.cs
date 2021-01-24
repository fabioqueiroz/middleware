using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc;
using Middleware.Api.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;

namespace Middleware.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class RequestController : Controller
    {
        public RequestController()
        {
            
        }

        [HttpGet]
        public IActionResult Get()
        {
            return Ok("The request reached the controller");
        }

        [HttpGet("json")]
        public IActionResult GetJson()
        {
            return Json("Json test");
        }

        [HttpPost]
        public IActionResult PostTest([FromBody] DeviceModel device)
        {
            return Ok();
        }
    }
}
