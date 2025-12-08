using Microsoft.AspNetCore.Mvc;
using odev.dagitim.portali.data;
using System.Linq;

namespace odev.dagitim.portali.Areas.User.Controllers
{
    [Area("User")]
    public class LoginController : Controller
    {
        private readonly AppDbContext _context;

        public LoginController(AppDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Index(string Numara, string Sifre)
        {
            var ogrenci = _context.Ogrenciler
                .FirstOrDefault(x => x.Numara == Numara && x.Sifre == Sifre);

            if (ogrenci != null)
            {
                HttpContext.Session.SetInt32("OgrenciId", ogrenci.Id);
                HttpContext.Session.SetString("OgrenciAd", ogrenci.AdSoyad);

                return RedirectToAction("Yukle", "Odev", new { area = "User" });
            }

            ViewBag.Hata = "Numara veya şifre yanlış";
            return View();
        }
        public IActionResult Cikis()
        {
            HttpContext.Session.Clear(); 
            return RedirectToAction("Index", "Home", new { area = "" }); 
        }

    }
}
