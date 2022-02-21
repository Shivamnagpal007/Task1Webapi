using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using TaskFrontEnd.Models;
using TaskFrontEnd.Repository.Irepository;

namespace TaskFrontEnd.Repository
{
    public class DepartmentRepository: Repository<Department>,IDepartmentRepository
    {
        private readonly IHttpClientFactory _httpClientFactory;
        public DepartmentRepository(IHttpClientFactory httpClientFactory, IHttpContextAccessor httpContextAccessor) : base(httpClientFactory, httpContextAccessor)
        {
            _httpClientFactory = httpClientFactory;
        }
       
        //public DepartmentRepository(IHttpClientFactory httpClientFactory) : base(httpClientFactory)
        //{
        //    _httpClientFactory = httpClientFactory;
        //}
    }
}
