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
            Department = new DepRepository(_context);
           
        }     
        public IDepRepository Department { get; private set; }
    }
}
