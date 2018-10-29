using OAuthenticationTest.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OAuthenticationTest.Repository
{
    public class DeptRepository
    {
        private IGenericRepository<Department> _repo;

        public DeptRepository(IGenericRepository<Department> repo)
        {
            _repo = repo;
        }
        public IEnumerable<Department> GetAll()
        {
            return _repo.All(new string[] { "Employees" }).ToList();
        }
    }
}