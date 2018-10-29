using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace OAuthenticationTest.Models
{
    public class Department
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public System.Guid DeptId { get; set; }
        public string DeptName { get; set; }
        public string Description { get; set; }
        public virtual ICollection<Employee> Employees { get; set; }
        
    }
}