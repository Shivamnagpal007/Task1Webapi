using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace taskWebapi.Repository.IRepository
{ 
        public interface IUnitOfWork
        {
        IDepRepository Department{ get; }
        }
}
