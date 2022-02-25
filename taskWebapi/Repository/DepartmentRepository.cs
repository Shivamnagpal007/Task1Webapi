using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using taskWebapi.Data;
using taskWebapi.Models;
using taskWebapi.Repository.IRepository;

namespace taskWebapi.Repository
{
    public class DepartmentRepository : Repository<Department>, IDepartmentRepository
    {
        private readonly ApplicationDbcontext _context;
        public DepartmentRepository(ApplicationDbcontext context) : base(context)
        {
            _context = context;

        }

        public bool Update(Department department)
        {
            _context.Update(department);
            return Save();
        }
        public bool DepartmentExistById(int depId)
        {
            return _context.Departments.Any(np => np.depId == depId);
        }

        public bool DepartmentExistByName(string dname)
        {
            return _context.Departments.Any(n => n.dname == dname);
        }
    }
}
    