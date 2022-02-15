using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using taskWebapi.Models;
//using taskWebapi.Migrations;

namespace taskWebapi.Repository.IRepository
{
   public interface IDsgRepository
    {
        ICollection<Designation> Getdsg();// Display
        Designation Getdsg(int dsgId);// Find
        bool Createdsg(Designation tbdsg);// Create
        bool Updatedsg(Designation tbdsg);// Update
        bool Deletedsg(Designation tbdsg);// Delete
        bool Save();// Save
        bool dsgexist(int dsgId);// Find by Id & also using function Overloading
        bool dsgexist(string dsgname);// Find by Name & also using  function Overloading
    }
}
