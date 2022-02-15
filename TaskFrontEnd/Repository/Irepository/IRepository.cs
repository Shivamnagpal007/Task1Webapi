using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace  TaskFrontEnd.Repository.Irepository
{
   public interface IRepository<T> where T:class
    {
      
        Task<T> GetAsync(string url, int id); // Find
        Task<List<T>> Getasync(string url, int id);
        Task<IEnumerable<T>> GetAllAsync(string url); //Display
        //IEnumerable<T> GetAll(
        //   Expression<Func<T, bool>> filter = null,
        //   Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
        //   string includeProperties = null
        //);
        Task<bool> CreateAsync(string url, T objtoCreate); //Create
        Task<bool> UpdateAsync(string url, T objtoUpdate); //Update
        Task<bool> DeleteAsync(string url, int id); //Delete
        Task<bool> DeleteAsyncEmployee(string url, int Empid,int Depid); //Delete
    }
}
