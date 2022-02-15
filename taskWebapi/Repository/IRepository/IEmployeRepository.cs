using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using taskWebapi.Models;

namespace taskWebapi.Repository.IRepository
{
    public interface IEmployeRepository
    {
        ICollection<Employee> GetEmploye();// Display
        ICollection<Employee> GetEmployeinDsg(int empId);
        Employee GetEmployee(int empId);// Find
        bool CreateEmployee(Employee tbemployee);// Create
        bool UpdateEmployee(Employee tbemployee);// Update
        bool DeleteEmployee(Employee tbemployee);// Delete
        bool Save();// Save
        //bool depexist(int depId);// Find by Id & also using function Overloading
        //bool depexist(string depname);// Find by Name & also using  function Overloading
    }
}
