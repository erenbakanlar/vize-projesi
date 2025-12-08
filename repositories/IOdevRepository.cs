using odev.dagitim.portali.models;

namespace odev.dagitim.portali.repositories
{
    public interface IOdevRepository
    {
        List<Odev> TumunuGetir();
        Odev IdyeGoreGetir(int id);
        void Ekle(Odev odev);
        void Kaydet();
    }
}
