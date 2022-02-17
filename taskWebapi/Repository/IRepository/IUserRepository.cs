using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using taskWebapi.Data;
using taskWebapi.Models;
using taskWebapi.Models.ViewModel;

namespace taskWebapi.Repository.IRepository
{ 
    public interface IUserRepository
    {
        bool IsUniqueUser(string userName);
        Task<ApplicationUser> Register(User user);
        Task<ApplicationUser> Authenticate(authenticateViewModel authenticateViewModel);


    }
}
