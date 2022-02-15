using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using TaskFrontEnd.Models;
using TaskFrontEnd.Repository.Irepository;

namespace TaskFrontEnd.Repository
{
    public class DesignationRepository:Repository<Designation>,IDesignationRepository
    {
        private readonly IHttpClientFactory _httpClientFactory;
        public DesignationRepository(IHttpClientFactory httpClientFactory) : base(httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }
    }
}
