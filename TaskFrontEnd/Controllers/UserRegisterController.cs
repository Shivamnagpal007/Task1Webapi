using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using TaskFrontEnd.Models;
using TaskFrontEnd.Models.ViewModel;
using TaskFrontEnd.Repository.Irepository;

namespace TaskFrontEnd.Controllers
{
    public class UserRegisterController : Controller
    {
        private readonly IUserRepository _userRepository;
        private readonly IHttpClientFactory _httpClientFactory;

        public UserRegisterController(IUserRepository userRepository, IHttpClientFactory httpClientFactory)
        {
            _userRepository = userRepository;
            _httpClientFactory = httpClientFactory;
        }
        public IActionResult userRegister()=> View();
       
        [HttpPost]
        public async Task<IActionResult> userRegister(User user)
        {

            if (ModelState.IsValid)
            {
                await _userRepository.CreateAsync(SD.UserRegister, user);
            }
            return RedirectToAction("userRegister");
                     
        }
        public IActionResult LoginUser()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> LoginUserAuthorized(authenticateViewModel authenticateViewModel)
        {
            var client = _httpClientFactory.CreateClient();
            if (ModelState.IsValid)
            {
                using (var httpClient = new HttpClient())
                {
                    StringContent stringContent = new StringContent(JsonConvert.SerializeObject(authenticateViewModel), Encoding.UTF8, "application/json");
                    using (var result = await httpClient.PostAsync("https://localhost:44335/api/login/authenticate/", stringContent))
                    {
                        var UserDetails = await result.Content.ReadAsStringAsync();
                        var Username = JsonConvert.DeserializeObject<User>(UserDetails);
                        string token = await result.Content.ReadAsStringAsync();
                        // HttpContext.Session.SetString("JWToken", token);
                        HttpContext.Session.SetString(SD.newtoken, Username.Token);
                        HttpContext.Session.SetString(SD.UserDetails, Username.Username);
                        //CookieSet(SD.Cookiesdata, token, 10);
                    }
                    
                }
            }
            return Redirect("~/Home/Index");

        }
        //public void CookieSet(string key, string value, int? expireTime)
        //{
        //    CookieOptions option = new CookieOptions();
        //    if (expireTime.HasValue)
        //        option.Expires = DateTime.Now.AddMinutes(expireTime.Value);
        //    else
        //        option.Expires = DateTime.Now.AddMilliseconds(10);
        //    Response.Cookies.Append(key, value, option);
        //}
        //public void Remove(string key)
        //{
        //    Response.Cookies.Delete(SD.Cookiesdata);
        //}
    }
}
