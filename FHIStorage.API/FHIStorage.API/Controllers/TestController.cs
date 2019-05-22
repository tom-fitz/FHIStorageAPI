using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace FHIStorage.API.Controllers
{
    [Route("api")]
    public class TestController : Controller
    {

        private IConfiguration _config;

        public TestController(IConfiguration configuration)
        {
            _config = configuration;
        }

        [HttpGet("test")]
        public string getTestController(IConfiguration _config)
        {
            return _config["DBConnectionString"];
        }
    }
}
