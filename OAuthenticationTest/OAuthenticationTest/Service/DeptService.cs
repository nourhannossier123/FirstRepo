using AutoMapper;
using OAuthenticationTest.BModel;
using OAuthenticationTest.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OAuthenticationTest.Service
{
    public class DeptService
    {
        public DeptRepository _repo;
        public IMapper _mapper;
        public DeptService(DeptRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }
        public List<DepartmentModel> GetAll()
        {
            try
            {
                var Depts = _repo.GetAll();
                return _mapper.Map<List<DepartmentModel>>(Depts);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}