using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using taskWebapi.Data;
using taskWebapi.Models;
using taskWebapi.Repository.IRepository;

namespace taskWebapi.Repository
{
    public class DesignationRepository : Repository<Designation>, IDesignationRepository
    {
        private readonly ApplicationDbcontext _context;
        public DesignationRepository(ApplicationDbcontext context) : base(context)
        {
            _context = context;

        }

        public bool Update(Designation designation)
        {
            _context.Update(designation);
            return Save();
        }
        public bool DesignationExistById(int dsgId)
        {
            return _context.Designations.Any(np => np.dsgId == dsgId);
        }

        public bool DesignationExistByName(string dsgname)
        {
            return _context.Designations.Any(n => n.dsgname == dsgname);
        }
    }
}

