using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;

namespace odev.dagitim.portali.Hubs
{
    // [VIDEO - SIGNALR HUB - BAŞLANGIÇ]
    public class OdevHub : Hub
    {
        // Admin paneline ödev eklendiğinde bildirim gönder
        public async Task OdevEklendi(string baslik, string aciklama)
        {
            await Clients.All.SendAsync("YeniOdevBildirimi", baslik, aciklama);
        }

        // Öğrenci ödev yüklediğinde admin'e bildirim gönder
        public async Task OgrenciOdevYukledi(string ogrenciAd, string odevBaslik)
        {
            await Clients.All.SendAsync("OdevYuklemeBildirimi", ogrenciAd, odevBaslik);
        }
    }
    // [VIDEO - SIGNALR HUB - BİTİŞ]
}
