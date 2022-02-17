using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using taskWebapi.Data;
using taskWebapi.Models;
using taskWebapi.Models.ViewModel;
using taskWebapi.Repository.IRepository;

namespace taskWebapi.Controllers
{
    [Route("api/login")]
    [ApiController]
    public class UserController : Controller
    {
        private readonly IUserRepository _userRepository;
       
        public UserController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
           
        }
          
        [HttpPost("authenticate")]
        public async Task<IActionResult> Authenticate([FromBody] authenticateViewModel authenticateViewModel)
        {
            var user =await _userRepository.Authenticate(authenticateViewModel);
            if (user == null)
                return BadRequest("Wrong username and password");
            else
               return Ok(user);
        }
        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> Register(User user)
        {
            var registeruser = await _userRepository.Register(user);
            if (registeruser == null)
                return BadRequest(new { message = "User already exist" });
            return Ok(registeruser);
        }

    }
}
