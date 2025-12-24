using odev.dagitim.portali.models;

namespace odev.dagitim.portali.repositories
{
    public interface IStudentRepository
    {
        List<Student> TumunuGetir();
        Student IdyeGoreGetir(int id);
        Student NumarayaGoreGetir(string numara);
        void Ekle(Student ogrenci);
        void Sil(int id);
        void Kaydet();
    }
}
