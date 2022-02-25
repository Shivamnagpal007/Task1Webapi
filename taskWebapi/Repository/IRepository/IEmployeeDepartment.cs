using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using taskWebapi.Models;
using taskWebapi.Models.Dtos;

namespace taskWebapi.Repository.IRepository
{
    public interface IEmployeeDepartment : IRepository<EmployeeDepartmentRepository>,IFuncForEmployeDepartment
    {
        
    }
}
