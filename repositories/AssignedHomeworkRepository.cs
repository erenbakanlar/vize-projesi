using odev.dagitim.portali.data;
using odev.dagitim.portali.models;
using System.Collections.Generic;
using System.Linq;

namespace odev.dagitim.portali.repositories
{
    public class AssignedHomeworkRepository : IAssignedHomeworkRepository
    {
        private readonly AppDbContext _context;

        public AssignedHomeworkRepository(AppDbContext context)
        {
            _context = context;
        }

        public List<AssignedHomework> GetAll()
        {
            return _context.DagitilanOdevler.ToList();
        }

        public void Add(AssignedHomework odev)
        {
            _context.DagitilanOdevler.Add(odev);
            _context.SaveChanges();
        }

        public void Delete(int id)
        {
            var odev = _context.DagitilanOdevler.Find(id);
            if (odev != null)
            {
                _context.DagitilanOdevler.Remove(odev);
                _context.SaveChanges();
            }
        }
    }
}
