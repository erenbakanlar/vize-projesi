using odev.dagitim.portali.data;
using odev.dagitim.portali.models;

namespace odev.dagitim.portali.repositories
{
    public class StudentRepository : IStudentRepository
    {
        private readonly AppDbContext _context;

        public StudentRepository(AppDbContext context)
        {
            _context = context;
        }

        public List<Student> TumunuGetir()
        {
            return _context.Ogrenciler.ToList();
        }

        public Student IdyeGoreGetir(int id)
        {
            return _context.Ogrenciler.Find(id);
        }

        public Student NumarayaGoreGetir(string numara)
        {
            return _context.Ogrenciler
                .FirstOrDefault(o => o.Email.ToLower() == numara.ToLower());
        }

        public void Ekle(Student ogrenci)
        {
            _context.Ogrenciler.Add(ogrenci);
        }

        public void Sil(int id)
        {
            var ogrenci = _context.Ogrenciler.Find(id);
            if (ogrenci != null)
            {
                _context.Ogrenciler.Remove(ogrenci);
            }
        }

        public void Kaydet()
        {
            _context.SaveChanges();
        }
    }
}
