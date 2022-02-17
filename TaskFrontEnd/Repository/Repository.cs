using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Nancy.Json;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using TaskFrontEnd.Models;
using TaskFrontEnd.Repository.Irepository;

namespace TaskFrontEnd.Repository
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly ISession _session;

        public Repository(IHttpClientFactory httpClientFactory, IHttpContextAccessor HttpContextAccessor)
        {
            _httpClientFactory = httpClientFactory;
            _session = HttpContextAccessor.HttpContext.Session;
        }
        public async Task<bool> CreateAsync(string url, T objtoCreate)
        {
            var accessToken = _session.GetString(SD.newtoken);
            var request = new HttpRequestMessage(HttpMethod.Post, url);
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
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
        public async Task<bool> CreateUser(string url, T objtoCreate)
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
            var accessToken = _session.GetString(SD.newtoken);
            var request = new HttpRequestMessage(HttpMethod.Delete, url + id.ToString());
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
            var client = _httpClientFactory.CreateClient();

            HttpResponseMessage response = await client.SendAsync(request);
            if (response.StatusCode == System.Net.HttpStatusCode.OK)
                return true;
            else 
                return false;
        }
        public async Task<bool> DeleteAsyncEmployee(string url, int Empid, int Depid)
        {
            var accessToken = _session.GetString(SD.newtoken);
            var request = new HttpRequestMessage(HttpMethod.Delete, url + Empid.ToString() + Depid.ToString());
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
            var client = _httpClientFactory.CreateClient();

            HttpResponseMessage response = await client.SendAsync(request);
            if (response.StatusCode == System.Net.HttpStatusCode.OK)
                return true;
            else
                return false;
        }

        public async Task<IEnumerable<T>> GetAllAsync(string url)
        {
            var accessToken = _session.GetString(SD.newtoken);
            var request = new HttpRequestMessage(HttpMethod.Get, url);
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
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
            var accessToken = _session.GetString(SD.newtoken);
            var request = new HttpRequestMessage(HttpMethod.Get, url + id.ToString());
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
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
            var accessToken = _session.GetString(SD.newtoken);
            var request = new HttpRequestMessage(HttpMethod.Get, url + id.ToString());
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
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
            var accessToken = _session.GetString(SD.newtoken);
            var request = new HttpRequestMessage(HttpMethod.Put,url);
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
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
