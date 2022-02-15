using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using taskWebapi.Models;
using taskWebapi.Models.Dtos;

namespace taskWebapi.Repository.IRepository
{
  public interface IempDepRepository
    {
        List<findempdempdto> Getempdep();// Display
        List<findempdempdto> GetempdepId(int empId);// Find
        EmployeeDepartment Get(int EmpId,int DepId);
        //EmployeeDepartment GetEmploye(int EmpId);
        void Update(empdepdto empdepdto);
        //void Add(EmployeeDepartment employeeDepartment);
        bool Createempdep(empdepdto empdepdto);// Create
        //bool Updateempdep(EmployeeDepartment empdemp);// Update

        void Deleteempdep(int id);// Delete
       // bool Deleteempdep(EmployeeDepartment empdemp);// Delete
        bool Save();// Save

    }
}
