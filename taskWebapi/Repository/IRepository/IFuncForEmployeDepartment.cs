using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using taskWebapi.Models;
using taskWebapi.Models.Dtos;

namespace taskWebapi.Repository.IRepository
{
   public interface IFuncForEmployeDepartment
    {
        List<FindEmployeeDepartment> Getempdep();// Display 
        bool Create(Models.Dtos.EmployeeDepartmentDto empdepdto);// Create
        List<FindEmployeeDepartment> GetempdepId(int empId);// Find
        void Delete(int id);// Delete
        void Update(Models.Dtos.EmployeeDepartmentDto empdepdto);
        Models.EmployeeDepartment Get(int EmpId, int DepId);
    }
}
