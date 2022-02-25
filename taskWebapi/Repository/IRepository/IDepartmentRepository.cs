using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using taskWebapi.Models;

namespace taskWebapi.Repository.IRepository
{
    public interface IDepartmentRepository : IRepository<Department>
    {
        bool Update(Department department);
         bool DepartmentExistById(int depId);// Find by Id & also using function Overloading
        bool DepartmentExistByName(string depname);// Find by Name & also using  function Overloading

    }
}
