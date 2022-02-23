
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using taskWebapi.Data;
using taskWebapi.Models;
using taskWebapi.Models.ViewModel;
using taskWebapi.Repository.IRepository;

namespace taskWebapi.Repository
{

    public class UserRepository :IUserRepository
    {     
        private readonly ApplicationDbcontext _context;
        private readonly AppSettings _appSettings;
        private readonly ApplicationUserManager _applicationUserManager;
        private readonly ApplicationSignInManager _applicationSignInManager;
        public UserRepository(ApplicationDbcontext context, IOptions<AppSettings> appSettings, ApplicationUserManager applicationUserManager, ApplicationSignInManager applicationSignInManager)
        {
            _context = context;
            _appSettings = appSettings.Value;
            _applicationUserManager = applicationUserManager;
            _applicationSignInManager = applicationSignInManager;
        }
        public async Task<ApplicationUser> Authenticate(authenticateViewModel authenticateViewModel)
        {
            var result = await _applicationSignInManager.PasswordSignInAsync(authenticateViewModel.Username, authenticateViewModel.Password, false, false);
            if (result.Succeeded)
            {
                var applicationUser = await _applicationUserManager.FindByNameAsync(authenticateViewModel.Username);
                applicationUser.PasswordHash = null;
                //  JWT Token
                if (await _applicationUserManager.IsInRoleAsync(applicationUser, SD.Role_Admin))
                    applicationUser.Role = SD.Role_Admin;              
                var tokenHandler = new JwtSecurityTokenHandler();
                var key = System.Text.Encoding.ASCII.GetBytes(_appSettings.Secret);

                var tokenDescriptor = new SecurityTokenDescriptor()
                {
                    Subject = new ClaimsIdentity(new Claim[]
                  {
                  new Claim(ClaimTypes.Name,applicationUser.Id),
                new Claim(ClaimTypes.Email,applicationUser.Email),
                   new Claim(ClaimTypes.Role,applicationUser.Role)
                  }),
                    Expires = DateTime.UtcNow.AddHours(30),
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key),
                  SecurityAlgorithms.HmacSha256Signature)
                };

                var token = tokenHandler.CreateToken(tokenDescriptor);
                applicationUser.Token = tokenHandler.WriteToken(token);

                return applicationUser;
            }
            else
                return null;
        }

        
        public bool IsUniqueUser(string userName)
        {
            var user = _context.Users.FirstOrDefault(u => u.Username == userName);
            if (user == null)
                return true;
            else
                return false;
        }

        public async Task<ApplicationUser> Register(User user)
        {
            if (await _applicationUserManager.FindByNameAsync(user.Username) == null)
            {
                var User = new ApplicationUser();
                User.FirstName = user.FirstName;
                User.LastName = user.LastName;
                User.UserName = user.Username;
                User.Email = user.Email;
                User.PasswordHash = user.Password;

                var chkuser = await _applicationUserManager.CreateAsync(User, User.PasswordHash);
                if (chkuser.Succeeded)
                {
                    await _applicationUserManager.AddToRoleAsync(User, "Admin");                
                }

                return User;
            }
            else
                return null;

        }
    }
}
