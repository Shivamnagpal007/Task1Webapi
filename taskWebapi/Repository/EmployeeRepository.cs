using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using taskWebapi.Data;
using taskWebapi.Models;
using taskWebapi.Repository.IRepository;

namespace taskWebapi.Repository
{
    public class EmployeeRepository : IEmployeRepository
    {
        private readonly ApplicationDbcontext _context;
        public EmployeeRepository(ApplicationDbcontext context)
        {
            _context = context;
        }
        public bool CreateEmployee(Employee tbemployee)
        {
            _context.Employees.Add(tbemployee);
            return Save();
        }

        public bool DeleteEmployee(Employee tbemployee)
        {
            _context.Employees.Remove(tbemployee);
            return Save();
        }

        public ICollection<Employee> GetEmploye()
        {
            var data= _context.Employees.Include(t => t.Designation ).ToList();
            return data;
        }

        public Employee GetEmployee(int empId)
        {
            var data =_context.Employees.Include(t => t.Designation).FirstOrDefault(t => t.empId == empId);
            return data;
        }

        public ICollection<Employee> GetEmployeinDsg(int dsgId)
        {
            return _context.Employees.Include(t => t.Designation).Where(t => t.dsgId == dsgId).ToList();
        }

        public bool Save()
        {
            return _context.SaveChanges() == 1 ? true : false;
        }

        public bool UpdateEmployee(Employee tbemployee)
        {
            _context.Employees.Update(tbemployee);
            return Save();
        }
    }
}
