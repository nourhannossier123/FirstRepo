using Newtonsoft.Json;
using OAuthenticationTest.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OAuthenticationTest.BModel
{
    public class DepartmentModel
    {
        public System.Guid DeptId { get; set; }
        public string DeptName { get; set; }
        public string Description { get; set; }

        public ICollection<EmployeeModel> Employees { get; set; }
    }
}