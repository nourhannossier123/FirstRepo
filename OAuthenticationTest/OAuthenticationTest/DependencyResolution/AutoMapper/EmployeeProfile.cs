using AutoMapper;
using OAuthenticationTest.BModel;
using OAuthenticationTest.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OAuthenticationTest.DependencyResolution.AutoMapper
{
    public class EmployeeProfile:Profile
    {
        protected override void Configure()
        {
            CreateMap<Employee, EmployeeModel>().ForMember(dest => dest.deptname, opt => opt.MapFrom(s => s.Department.DeptName)); ;
            CreateMap<EmployeeModel, Employee>();
        }
    }
}