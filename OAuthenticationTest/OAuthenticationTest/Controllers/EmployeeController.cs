using OAuthenticationTest.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace OAuthenticationTest.Controllers
{
    public class EmployeeController : ApiController
    {
        public EmployeeService _service;
        public EmployeeController(EmployeeService service)
        {
            _service = service;
        }
        [Route("all")]
        [HttpGet]
        public IHttpActionResult GetAll()
        {
            try
            {
                var Emps = _service.GetAll();
                return Ok(Emps);
            }
            catch(Exception ex)
            {
                return InternalServerError();
            }
        }
    }
}
