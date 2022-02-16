using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using taskWebapi.Data;
using taskWebapi.Models;
using taskWebapi.Repository.IRepository;

namespace taskWebapi.Repository
{
  
        public class UserRepository : IUserRepository
        {
            private readonly ApplicationDbcontext _context;
            private readonly AppSettings _appSettings;
            public UserRepository(ApplicationDbcontext context, IOptions<AppSettings> appSettings)
            {
                _context = context;
                _appSettings = appSettings.Value;
            }
            public User Authenticate(string userName, string password)
            {
                var userInDb = _context.Users.FirstOrDefault
                       (u => u.Username == userName && u.Password == password);
                if (userInDb == null)
                    return null;
                //JWT Authentication
                var tokenHandler = new JwtSecurityTokenHandler();
                var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
                var tokenDescritor = new SecurityTokenDescriptor()
                {
                    Subject = new ClaimsIdentity(new Claim[]
                    {
                    new Claim(ClaimTypes.Name, userInDb.Id.ToString()),
                        //new Claim(ClaimTypes.Role, userInDb.Role)
                    }),
                    Expires = DateTime.UtcNow.AddDays(7),
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)

                };
                var token = tokenHandler.CreateToken(tokenDescritor);
                userInDb.Token = tokenHandler.WriteToken(token);
                userInDb.Password = " ";

                //userInDb.Password = SD.ConvertToEncrypt( password);
                return userInDb;
            }

            public bool IsUniqueUser(string userName)
            {
                var user = _context.Users.FirstOrDefault(u => u.Username == userName);
                if (user == null)
                    return true;
                else
                    return false;
            }

            public User UserRegister(User user)
            {
                    User userdata = new User()
                    {
                        FirstName=user.FirstName,
                        LastName=user.LastName,
                        Email=user.Email,
                        Username = user.Username,
                        Password = user.Password,

                    };
                    _context.Users.Add(userdata);
                    _context.SaveChanges();
                    //    //JWT Authentication
                    //    var tokenHandler = new JwtSecurityTokenHandler();
                    //    var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
                    //    var tokenDescritor = new SecurityTokenDescriptor()
                    //    {
                    //        Subject = new ClaimsIdentity(new Claim[]
                    //        {
                    //    new Claim(ClaimTypes.Name, log.Id.ToString()),
                    //            //new Claim(ClaimTypes.Role, log.Role)
                    //        }),
                    //        Expires = DateTime.UtcNow.AddDays(7),
                    //        SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)

                    //    };
                    //    var token = tokenHandler.CreateToken(tokenDescritor);
                    //    log.Token = tokenHandler.WriteToken(token);
                    //    log.Password = " ";

                    //    return log;

                    //}
                    //else
                    return userdata;
                }
            }
        }

