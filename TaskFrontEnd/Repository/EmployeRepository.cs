using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using TaskFrontEnd.Models;
using TaskFrontEnd.Repository.Irepository;

namespace TaskFrontEnd.Repository
{
    public class EmployeRepository:Repository<Employee>,IEmployeRepository
    {
        private readonly IHttpClientFactory _httpClientFactory;
        public EmployeRepository(IHttpClientFactory httpClientFactory) : base(httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }
    }
}
