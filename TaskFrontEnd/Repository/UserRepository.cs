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
    public class UserRepository : Repository<User>, IUserRepository
    {
        private readonly IHttpClientFactory _httpClientFactory;
        public UserRepository(IHttpClientFactory httpClientFactory, IHttpContextAccessor httpContextAccessor) : base(httpClientFactory, httpContextAccessor)
        {
            _httpClientFactory = httpClientFactory;
        }
        //private readonly IHttpClientFactory _httpClientFactory;
        //public UserRepository(IHttpClientFactory httpClientFactory) : base(httpClientFactory)
        //{
        //    _httpClientFactory = httpClientFactory;
        //}
    }
}
