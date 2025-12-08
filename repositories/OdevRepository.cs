using odev.dagitim.portali.data;
using odev.dagitim.portali.models;

namespace odev.dagitim.portali.repositories
{
    public class OdevRepository : IOdevRepository
    {
        private readonly AppDbContext _context;

        public OdevRepository(AppDbContext context)
        {
            _context = context;
        }

        public List<Odev> TumunuGetir()
        {
            return _context.Odevler.ToList();
        }

        public Odev IdyeGoreGetir(int id)
        {
            return _context.Odevler.Find(id);
        }

        public void Ekle(Odev odev)
        {
            _context.Odevler.Add(odev);
        }

        public void Kaydet()
        {
            _context.SaveChanges();
        }
    }
}
