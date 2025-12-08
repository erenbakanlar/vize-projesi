using Microsoft.AspNetCore.Mvc;
using odev.dagitim.portali.data;
using odev.dagitim.portali.models;

namespace odev.dagitim.portali.Areas.User.Controllers
{
    [Area("User")]
    public class OdevController : Controller
    {
        private readonly AppDbContext _context;

        public OdevController(AppDbContext context)
        {
            _context = context;
        }

        public IActionResult Yukle()
        {
            var ogrenciId = HttpContext.Session.GetInt32("OgrenciId");

            if (ogrenciId == null)
            {
                return RedirectToAction("Index", "Login", new { area = "User" });
            }

            return View();
        }

        [HttpPost]
        public IActionResult Yukle(IFormFile dosya)
        {
            var ogrenciId = HttpContext.Session.GetInt32("OgrenciId");

            if (ogrenciId == null)
            {
                return RedirectToAction("Index", "Login", new { area = "User" });
            }

            if (dosya != null && dosya.Length > 0)
            {
                var uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/odevler");

                if (!Directory.Exists(uploadsFolder))
                    Directory.CreateDirectory(uploadsFolder);

                var fileName = Guid.NewGuid().ToString() + Path.GetExtension(dosya.FileName);
                var filePath = Path.Combine(uploadsFolder, fileName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    dosya.CopyTo(stream);
                }

                Odev yeniOdev = new Odev
                {
                    OgrenciId = ogrenciId.Value,
                    DosyaYolu = "/odevler/" + fileName,
                    YuklemeTarihi = DateTime.Now
                };

                _context.Odevler.Add(yeniOdev);
                _context.SaveChanges();

                ViewBag.Mesaj = "Ödev başarıyla yüklendi!";
            }

            return View();
        }
    }
}
