using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using taskWebapi.Data;
using taskWebapi.Models;
using taskWebapi.Repository.IRepository;

namespace taskWebapi.Repository
{
    public class DsgRepository : IDsgRepository
    {
        private readonly ApplicationDbcontext _context;
        public DsgRepository(ApplicationDbcontext context)
        {
            _context = context;
        }

        public bool Createdsg(Designation tbdsg)
        {
            _context.Designations.Add(tbdsg);
            return Save();
        }

        public bool Deletedsg(Designation tbdsg)
        {
            _context.Designations.Remove(tbdsg);
            return Save();
        }

        public bool dsgexist(int dsgId)
        {
            return _context.Designations.Any(np => np.dsgId == dsgId);
        }

        public bool dsgexist(string dsgname)
        {
            return _context.Designations.Any(n => n.dsgname == dsgname);
        }

        public ICollection<Designation> Getdsg()
        {
            return _context.Designations.ToList();
        }

        public Designation Getdsg(int dsgId)
        {
            return _context.Designations.Find(dsgId);
        }

        public bool Save()
        {
            return _context.SaveChanges() == 1 ? true : false;
        }

        public bool Updatedsg(Designation tbdsg)
        {
            _context.Designations.Update(tbdsg);
            return Save();
        }

    }
}
