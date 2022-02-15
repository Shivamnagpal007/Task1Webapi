using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using taskWebapi.Models;

namespace taskWebapi.Repository.IRepository
{ 
    public interface IUserRepository
    {
        bool IsUniqueUser(string userName);
        User UserRegister(User user);
        User Authenticate(string userName, string password);

    }
}
