using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using odev.dagitim.portali.data;
using odev.dagitim.portali.models;

namespace odev.dagitim.portali.Areas.User.Controllers
{
    [Area("User")]
    [Authorize]
    public class OdevController : Controller
    {
        private readonly AppDbContext _context;

        public OdevController(AppDbContext context)
        {
            _context = context;
        }

       
        public IActionResult Yukle()
        {
            return View();
        }

       
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Yukle(IFormFile dosya)
        {
            if (dosya == null || dosya.Length == 0)
            {
                ViewBag.Mesaj = "Dosya seçmedin.";
                return View();
            }

            var uploadFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "odevler");
            if (!Directory.Exists(uploadFolder))
                Directory.CreateDirectory(uploadFolder);

            var fileName = Guid.NewGuid().ToString() + Path.GetExtension(dosya.FileName);
            var filePath = Path.Combine(uploadFolder, fileName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await dosya.CopyToAsync(stream);
            }

            var yeniOdev = new Odev
            {
                OgrenciId = 0,
                DosyaYolu = "/odevler/" + fileName,
                YuklemeTarihi = DateTime.Now
            };

            _context.Odevler.Add(yeniOdev);
            await _context.SaveChangesAsync();

            ViewBag.Mesaj = "Ödev başarıyla yüklendi!";
            return View();
        }
    }
}
