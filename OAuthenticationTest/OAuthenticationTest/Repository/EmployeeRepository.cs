using OAuthenticationTest.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OAuthenticationTest.Repository
{
    public class EmployeeRepository
    {
        private IGenericRepository<Employee> _repo;

        public EmployeeRepository(IGenericRepository<Employee> repo)
        {
            _repo = repo;
        }
        public IEnumerable<Employee> GetAll()
        {
            return _repo.All(new string[] { "Department" }).ToList();
        }
    }
}