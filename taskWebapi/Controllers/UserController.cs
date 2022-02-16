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
        public IActionResult Authenticate([FromBody] authenticateViewModel authenticateViewModel)
        {
            var user = _userRepository.Authenticate(authenticateViewModel.Username, authenticateViewModel.Password);
            if (user == null)
                return BadRequest("Wrong username and password");
            return Ok(user);
        }
        [HttpPost("register")]
        public IActionResult Register([FromBody] User user)
        {

            if (ModelState.IsValid)
            {
                var isUniquelogin = _userRepository.IsUniqueUser(user.Username);
                if (!isUniquelogin)
                    return BadRequest("Username must be unique");
                var userInfo = _userRepository.UserRegister(user);
                if (userInfo == null)
                    return BadRequest();
                return Ok(userInfo);
            }
            else
                return BadRequest();

        }

    }
}
