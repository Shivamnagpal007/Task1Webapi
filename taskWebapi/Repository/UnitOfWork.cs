using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using taskWebapi.Data;
using taskWebapi.Repository.IRepository;

namespace taskWebapi.Repository
{
    public class UnitOfWork:IUnitOfWork
    {
        private readonly ApplicationDbcontext _context;
        public UnitOfWork(ApplicationDbcontext context)
        {
            _context = context;
            Department = new DepartmentRepository(_context);
            Designation = new DesignationRepository(_context);
            EmployeDepartment = new EmployeeDepartmentRepository(_context);
           
        }     
        public IDepartmentRepository Department { get; private set; }
        public IDesignationRepository Designation { get; private set; }
        public IEmployeeDepartment EmployeDepartment { get; private set; }
    }
}
