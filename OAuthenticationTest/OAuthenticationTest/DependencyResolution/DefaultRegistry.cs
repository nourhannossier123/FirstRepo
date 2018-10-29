// --------------------------------------------------------------------------------------------------------------------
// <copyright file="DefaultRegistry.cs" company="Web Advanced">
// Copyright 2012 Web Advanced (www.webadvanced.com)
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
//
// http://www.apache.org/licenses/LICENSE-2.0

// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace OAuthenticationTest.DependencyResolution {
    using StructureMap.Configuration.DSL;
    using StructureMap;
    using OAuthenticationTest.Models;
    using System.Data.Entity;
    using OAuthenticationTest.Repository;
    using System.Linq;
    using System;
    using OAuthenticationTest.Service;
    using global::AutoMapper;

    public class DefaultRegistry : Registry
    {

        public DefaultRegistry()
        {
            var profiles = from t in typeof(DefaultRegistry).Assembly.GetTypes()
                           where typeof(Profile).IsAssignableFrom(t)
                           select (Profile)Activator.CreateInstance(t);

            var config = new MapperConfiguration(cfg =>
            {
                foreach (var profile in profiles)
                {
                    cfg.AddProfile(profile);
                }
            });


            var mapper = config.CreateMapper();

            For<IMapperConfiguration>().Use(config);
            For<IMapper>().Use(mapper);

            RegisterRepositories(mapper);
        }

    
        private void RegisterRepositories(IMapper mapper)
        {

            For<ApplicationDbContext>().Use<ApplicationDbContext>();
            Func<DbContext> contextCreator = () => new ApplicationDbContext() as DbContext;

            //  Injection For Employee Service

            GenericRepository<Employee> EmployeeGenericRepository = new GenericRepository<Employee>(contextCreator);
            EmployeeRepository employeeRepository = new EmployeeRepository(EmployeeGenericRepository);

            For<EmployeeService>().Use<EmployeeService>()
               .Ctor<EmployeeRepository>().Is(employeeRepository)
               .Ctor<IMapper>().Is(mapper);

            //inject Department Service
                GenericRepository<Department> DepartmentGenericRepository = new GenericRepository<Department>(contextCreator);
                DeptRepository deptRepository = new DeptRepository(DepartmentGenericRepository);

            For<DeptService>().Use<DeptService>()
               .Ctor<DeptRepository>().Is(deptRepository)
               .Ctor<IMapper>().Is(mapper);


        }
    }
}