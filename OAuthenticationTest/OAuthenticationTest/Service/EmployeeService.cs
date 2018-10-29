using AutoMapper;
using OAuthenticationTest.BModel;
using OAuthenticationTest.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OAuthenticationTest.Service
{
    public class EmployeeService
    {
        public EmployeeRepository _repo;
        public IMapper _mapper;
        public EmployeeService(EmployeeRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }
        public List<EmployeeModel> GetAll()
        {
            try
            {
                var Emps = _repo.GetAll();
                return _mapper.Map<List<EmployeeModel>>(Emps);
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
    }
}