using OAuthenticationTest.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace OAuthenticationTest.Controllers
{
    public class DeptController : ApiController
    {
        public DeptService _service;
        public DeptController(DeptService service)
        {
            _service = service;
        }
        [Route("api/dept/all")]
        [HttpGet]
        public IHttpActionResult GetAll()
        {
            try
            {
                var Depts = _service.GetAll();
                return Ok(Depts);
            }
            catch (Exception ex)
            {
                return InternalServerError();
            }
        }
    }
}
