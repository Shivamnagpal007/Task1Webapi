using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace taskWebapi.Repository.IRepository
{ 
        public interface IUnitOfWork
        {
        IDepartmentRepository Department{ get; }
        IDesignationRepository Designation { get; }
        IEmployeeDepartment EmployeDepartment { get; }
        }
}
