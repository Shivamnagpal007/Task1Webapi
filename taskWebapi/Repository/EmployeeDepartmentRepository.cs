using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using taskWebapi.Data;
using taskWebapi.Models;
using taskWebapi.Models.Dtos;
using taskWebapi.Repository;
using taskWebapi.Repository.IRepository;

namespace taskWebapi.Repository
{
    public class EmployeeDepartmentRepository : Repository<EmployeeDepartmentRepository>, IEmployeeDepartment,IFuncForEmployeDepartment
    {
        private readonly ApplicationDbcontext _context;
        public EmployeeDepartmentRepository(ApplicationDbcontext context) : base(context)
        {

            _context = context;
        }
        public bool Create(Models.Dtos.EmployeeDepartmentDto empdepdto)
        {
            Models.EmployeeDepartment employee = new Models.EmployeeDepartment()
            {

                depId = empdepdto.depId,

                Employee = new Employee
                {
                    ename = empdepdto.ename,
                    eadd = empdepdto.eadd,
                    esal = empdepdto.esal,
                    dsgId = empdepdto.dsgId
                },

            };
            _context.EmployeeDepartments.Add(employee);
            _context.SaveChanges();
            return Save();

        }
     

        public List<FindEmployeeDepartment> Getempdep()
        {

            var employee = (from ed in _context.EmployeeDepartments
                            join dep in _context.Departments
                            on ed.depId equals dep.depId
                            join emp in _context.Employees
                            on ed.empId equals emp.empId
                            join dsg in _context.Designations
                            on emp.dsgId equals dsg.dsgId
                            select new FindEmployeeDepartment
                            {
                                empId = emp.empId,
                                ename = emp.ename,
                                esal = emp.esal,
                                eadd = emp.eadd,
                                dsgname = dsg.dsgname,
                                dname = dep.dname
                            }).ToList();
            return employee;


        }
       
        public void Delete(int id)
        {
            var data = _context.Employees.Where(x => x.empId == id).ToList();
            _context.Employees.RemoveRange(data);
            var data1 = _context.EmployeeDepartments.FirstOrDefault(e => e.empId == id);
            _context.EmployeeDepartments.RemoveRange(data1);
            _context.SaveChanges();

        }

        public Models.EmployeeDepartment Get(int EmpId, int DepId)
        {
            var data = _context.EmployeeDepartments.Find(EmpId, DepId); // Find code 
            if (data != null)
            {
                data.depId = DepId;
                _context.EmployeeDepartments.Update(data);
                _context.SaveChanges();
            }
            return data;

        }

        public List<FindEmployeeDepartment> GetempdepId(int empId)
        {
            var employee = (from ed in _context.EmployeeDepartments
                            join dep in _context.Departments
                            on ed.depId equals dep.depId
                            join emp in _context.Employees
                            on ed.empId equals emp.empId
                            join dsg in _context.Designations
                            on emp.dsgId equals dsg.dsgId
                            where emp.empId.Equals(empId)
                            select new FindEmployeeDepartment
                            {
                                empId = emp.empId,
                                ename = emp.ename,
                                esal = emp.esal,
                                eadd = emp.eadd,
                                dsgId = dsg.dsgId,
                                dsgname = dsg.dsgname,
                                depId = dep.depId,
                                dname = dep.dname
                            }).ToList();
            return employee;

        }

        public bool Save()
        {
            return _context.SaveChanges() == 1 ? true : false;
        }
        public void Update(Models.Dtos.EmployeeDepartmentDto empdepdto)
        {
            Employee employee = new Employee()
            {
                empId = empdepdto.empId,
                ename = empdepdto.ename,
                eadd = empdepdto.eadd,
                esal = empdepdto.esal,
                dsgId = empdepdto.dsgId
            };


            _context.Employees.Update(employee);
            _context.SaveChanges();
        }

    }
}


