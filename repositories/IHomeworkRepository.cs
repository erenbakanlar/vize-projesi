using odev.dagitim.portali.models;

namespace odev.dagitim.portali.repositories
{
    public interface IHomeworkRepository
    {
        List<Homework> TumunuGetir();
        List<Homework> OgrenciyeGoreGetir(int ogrenciId);
        Homework IdyeGoreGetir(int id);
        void Ekle(Homework odev);
        void Sil(int id);
        void Kaydet();
    }
}
