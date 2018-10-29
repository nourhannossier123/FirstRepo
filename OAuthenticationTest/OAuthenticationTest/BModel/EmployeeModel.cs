using Newtonsoft.Json;
using OAuthenticationTest.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OAuthenticationTest.BModel
{
    public class EmployeeModel
    {
        public System.Guid EmpId { get; set; }
        public string EmpName { get; set; }
        public string EmpAddress { get; set; }
        public string EmpPhone { get; set; }
        public string Email { get; set; }
        public Nullable<System.Guid> DeptId { get; set; }
        public string ImageURL { get; set; }
        public string deptname { set; get; }
    }
}