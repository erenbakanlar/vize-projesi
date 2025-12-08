using odev.dagitim.portali.models;

namespace odev.dagitim.portali.repositories
{
    public interface IOgrenciRepository
    {
        List<Ogrenci> TumunuGetir();
        Ogrenci IdyeGoreGetir(int id);
        void Ekle(Ogrenci ogrenci);
        void Kaydet();
    }
}
