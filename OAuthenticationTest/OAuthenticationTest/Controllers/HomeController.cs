using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace OAuthenticationTest.Controllers
{
    public class HomeController : ApiController
    {
        [AllowAnonymous]
        [Route("")]
        [HttpGet]
        public string Get()
        {
            return "value from Home".ToString();
        }
    }
}
