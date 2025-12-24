using odev.dagitim.portali.data;
using odev.dagitim.portali.models;

namespace odev.dagitim.portali.repositories
{
    public class HomeworkRepository : IHomeworkRepository
    {
        private readonly AppDbContext _context;

        public HomeworkRepository(AppDbContext context)
        {
            _context = context;
        }

        public List<Homework> TumunuGetir()
        {
            return _context.Odevler.ToList();
        }

        public List<Homework> OgrenciyeGoreGetir(int ogrenciId)
        {
            return _context.Odevler.Where(o => o.StudentId == ogrenciId).ToList();
        }

        public Homework IdyeGoreGetir(int id)
        {
            return _context.Odevler.Find(id);
        }

        public void Ekle(Homework odev)
        {
            _context.Odevler.Add(odev);
        }

        public void Sil(int id)
        {
            var odev = _context.Odevler.Find(id);
            if (odev != null)
            {
                _context.Odevler.Remove(odev);
            }
        }

        public void Kaydet()
        {
            _context.SaveChanges();
        }
    }
}
