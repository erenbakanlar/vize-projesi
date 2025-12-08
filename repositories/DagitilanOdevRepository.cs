using odev.dagitim.portali.data;
using odev.dagitim.portali.models;
using System.Collections.Generic;
using System.Linq;

namespace odev.dagitim.portali.repositories
{
    public class DagitilanOdevRepository : IDagitilanOdevRepository
    {
        private readonly AppDbContext _context;

        public DagitilanOdevRepository(AppDbContext context)
        {
            _context = context;
        }

        public List<DagitilanOdev> GetAll()
        {
            return _context.DagitilanOdevler.ToList();
        }

        public void Add(DagitilanOdev odev)
        {
            _context.DagitilanOdevler.Add(odev);
            _context.SaveChanges();
        }
    }
}
