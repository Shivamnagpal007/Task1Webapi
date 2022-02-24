using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace taskWebapi.Repository.IRepository
{
    public interface IRepository<T> where T : class
    {
        T Get(int Id);

        IEnumerable<T> GetAll();

        bool Add(T entity);

        bool Remove(T entity);

        bool Save();
    }
}
