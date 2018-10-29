using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace OAuthenticationTest.Models
{
    public class Employee
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public System.Guid EmpId { get; set; }
        public string EmpName { get; set; }
        public string EmpAddress { get; set; }
        public string EmpPhone { get; set; }
        public string Email { get; set; }
        [ForeignKey("Department")]
        public Nullable<System.Guid> DeptId { get; set; }
        public string ImageURL { get; set; }
        public virtual Department Department { get; set; }
      
    }
}