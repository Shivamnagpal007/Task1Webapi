using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using taskWebapi.Data;
using taskWebapi.Models;
using taskWebapi.Repository.IRepository;

namespace taskWebapi.Repository
{
    public class DepRepository : IDepRepository
    {
        private readonly ApplicationDbcontext _context;
        public DepRepository(ApplicationDbcontext context)
        {
            _context = context;
        }
        public bool Createdep(Department tbdep)
        {
            _context.Departments.Add(tbdep);
            return Save();
        }

        public bool Deletedep(Department tbdep)
        {

            _context.Departments.Remove(tbdep);
            return Save();
        }

        public bool depexist(int depId)
        {
            return _context.Departments.Any(np => np.depId == depId);
        }

        public bool depexist(string dname)
        {
            return _context.Departments.Any(n => n.dname == dname);
        }

        public ICollection<Department> Getdep()
        {
            return _context.Departments.ToList();
        }

        public Department Getdep(int depId)
        {
            return _context.Departments.Find(depId);
        }

        public bool Save()
        {
            return _context.SaveChanges() == 1 ? true : false;
        }

        public bool Updatedep(Department tbdep)
        {
            _context.Departments.Update(tbdep);
            return Save();
        }
    }
}
