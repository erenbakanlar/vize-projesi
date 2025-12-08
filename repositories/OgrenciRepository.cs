using odev.dagitim.portali.data;
using odev.dagitim.portali.models;

namespace odev.dagitim.portali.repositories
{
    public class OgrenciRepository : IOgrenciRepository
    {
        private readonly AppDbContext _context;

        public OgrenciRepository(AppDbContext context)
        {
            _context = context;
        }

        public List<Ogrenci> TumunuGetir()
        {
            return _context.Ogrenciler.ToList();
        }

        public Ogrenci IdyeGoreGetir(int id)
        {
            return _context.Ogrenciler.Find(id);
        }

        public void Ekle(Ogrenci ogrenci)
        {
            _context.Ogrenciler.Add(ogrenci);
        }

        public void Kaydet()
        {
            _context.SaveChanges();
        }
    }
}
