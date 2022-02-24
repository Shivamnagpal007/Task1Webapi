using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using taskWebapi.Data;
using taskWebapi.Repository.IRepository;

namespace taskWebapi.Repository
{
    public class Repository<T> : IRepository<T> where T : class
    {
        protected readonly ApplicationDbcontext _context;
        internal DbSet<T> dbset;
        public Repository(ApplicationDbcontext context)
        {
            _context = context;
            this.dbset = _context.Set<T>();
        }

        public bool Add(T entity)
        {
            dbset.Add(entity);
            return Save();
        }

        public T Get(int Id)
        {
            return dbset.Find(Id);
        }

        public IEnumerable<T> GetAll()
        {
            return dbset.ToList();
        }

        public bool Remove(T entity)
        {
            _context.Set<T>().Remove(entity);
            return Save();
        }

        public bool Save()
        {
            return _context.SaveChanges() == 1 ? true : false;
        }
    }
}
