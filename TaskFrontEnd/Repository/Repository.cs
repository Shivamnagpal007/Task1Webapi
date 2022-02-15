using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using TaskFrontEnd.Repository.Irepository;

namespace TaskFrontEnd.Repository
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly IHttpClientFactory _httpClientFactory;
    
        public Repository(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }
        public async Task<bool> CreateAsync(string url, T objtoCreate)
        {
            var request = new HttpRequestMessage(HttpMethod.Post, url);
            if (objtoCreate != null)
            { 
                request.Content = new StringContent(JsonConvert.SerializeObject(objtoCreate), Encoding.UTF8,
                "application/json"); 
            }
            var client = _httpClientFactory.CreateClient();
            HttpResponseMessage Response = await client.SendAsync(request);
            if (Response.StatusCode == System.Net.HttpStatusCode.Created)
                return true;
            else return false;
        }

        public async Task<bool> DeleteAsync(string url, int id)
        {
            var request = new HttpRequestMessage(HttpMethod.Delete, url + id.ToString());
            var client = _httpClientFactory.CreateClient();

            HttpResponseMessage response = await client.SendAsync(request);
            if (response.StatusCode == System.Net.HttpStatusCode.OK)
                return true;
            else 
                return false;
        }
        public async Task<bool> DeleteAsyncEmployee(string url, int Empid, int Depid)
        {
            var request = new HttpRequestMessage(HttpMethod.Delete, url + Empid.ToString() + Depid.ToString());
            var client = _httpClientFactory.CreateClient();

            HttpResponseMessage response = await client.SendAsync(request);
            if (response.StatusCode == System.Net.HttpStatusCode.OK)
                return true;
            else
                return false;
        }

        public async Task<IEnumerable<T>> GetAllAsync(string url)
        {
            var request = new HttpRequestMessage(HttpMethod.Get, url);
            var client = _httpClientFactory.CreateClient();
            HttpResponseMessage response = await client.SendAsync(request);
            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                var jsonstring = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<IEnumerable<T>>(jsonstring);
            }
            return null;
        }

        public async Task<T> GetAsync(string url, int id)
        {
            var request = new HttpRequestMessage(HttpMethod.Get, url + id.ToString());
            var client = _httpClientFactory.CreateClient();
            HttpResponseMessage response = await client.SendAsync(request);
            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                var jsonString = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<T>(jsonString);
            }
            return null;
        }

        public async Task<List<T>> Getasync(string url, int id)
        {
            var request = new HttpRequestMessage(HttpMethod.Get, url + id.ToString());
            var client = _httpClientFactory.CreateClient();
            HttpResponseMessage response = await client.SendAsync(request);
            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                var jsonString = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<List<T>>(jsonString);
            }
            return null;
        }

        public async Task<bool> UpdateAsync(string url, T objtoUpdate)
        {

           // var request = new HttpRequestMessage(HttpMethod.Put, url);
            var request = new HttpRequestMessage(HttpMethod.Put,url);

            if (objtoUpdate != null)
            { 
                request.Content = new StringContent(JsonConvert.SerializeObject(objtoUpdate), Encoding.UTF8, "application/json"); 
            }
            var client = _httpClientFactory.CreateClient();
            HttpResponseMessage Response = await client.SendAsync(request);
            if (Response.StatusCode == System.Net.HttpStatusCode.Created)
                return true;
            else
                return false;
        }
    }
}
