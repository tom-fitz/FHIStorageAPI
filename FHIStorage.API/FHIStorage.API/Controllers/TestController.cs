using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace FHIStorage.API.Controllers
{
    [Route("api/test")]
    public class TestController : Controller
    {

        private readonly IConfiguration _config;

        public TestController(IConfiguration configuration)
        {
            _config = configuration;
        }

        [HttpGet("")]
        public string getTestController()
        {
            return _config["StorageCredentials"];
        }
    }
}
