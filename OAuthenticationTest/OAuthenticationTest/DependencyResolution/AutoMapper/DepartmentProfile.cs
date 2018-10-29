using AutoMapper;
using OAuthenticationTest.BModel;
using OAuthenticationTest.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OAuthenticationTest.DependencyResolution.AutoMapper
{
    public class DepartmentProfile:Profile
    {
        protected override void Configure()
        {
            CreateMap<Department, DepartmentModel>();
            CreateMap<DepartmentModel, Department>();
        }
    }
}