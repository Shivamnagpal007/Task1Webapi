using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using taskWebapi.Models;
//using taskWebapi.Migrations;

namespace taskWebapi.Repository.IRepository
{
    public interface IDesignationRepository : IRepository<Designation>
    {
        bool Update(Designation designation);
      bool DesignationExistById(int dsgId);// Find by Id & also using function Overloading
        bool DesignationExistByName(string dsgname);// Find by Name & also using  function Overloading

    }
}
