using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using taskWebapi.Models;

namespace taskWebapi.Repository.IRepository
{
   public interface IDepRepository
    {
        ICollection<Department> Getdep();// Display
        Department Getdep(int depId);// Find
        bool Createdep(Department tbdep);// Create
        bool Updatedep(Department tbdep);// Update
        bool Deletedep(Department tbdep);// Delete
        bool Save();// Save
        bool depexist(int depId);// Find by Id & also using function Overloading
        bool depexist(string depname);// Find by Name & also using  function Overloading
    }
}
