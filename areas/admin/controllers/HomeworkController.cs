using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using odev.dagitim.portali.data;
using odev.dagitim.portali.repositories;

namespace odev.dagitim.portali.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class HomeworkController : Controller
    {
        private readonly IHomeworkRepository _odevRepository;
        private readonly AppDbContext _context;

        public HomeworkController(IHomeworkRepository odevRepository, AppDbContext context)
        {
            _odevRepository = odevRepository;
            _context = context;
        }

        public IActionResult Listele()
        {
            var odevler = _context.Odevler
                .Include(o => o.Student)
                .Include(o => o.AssignedHomework)
                .Where(o => o.StudentId > 0 && o.AssignedHomeworkId > 0)  // Sadece geçerli kayıtlar
                .ToList();
            return View(odevler);
        }

        [HttpPost]
        public IActionResult Sil(int id)
        {
            var odev = _odevRepository.IdyeGoreGetir(id);
            if (odev != null)
            {
                // Fiziksel dosyayı sil
                var dosyaAdi = odev.FilePath.Trim();
                var tamDosyaYolu = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "odevler", dosyaAdi);
                
                if (System.IO.File.Exists(tamDosyaYolu))
                {
                    System.IO.File.Delete(tamDosyaYolu);
                }

                // Veritabanından sil
                _odevRepository.Sil(id);
                _odevRepository.Kaydet();
                return Json(new { success = true });
            }
            return Json(new { success = false });
        }

        public IActionResult Indir(int id)
        {
            var odev = _odevRepository.IdyeGoreGetir(id);
            if (odev == null)
            {
                return NotFound("Ödev kaydı bulunamadı.");
            }

            // Veritabanında sadece dosya adı var (GUID.pdf), odevler klasörünü ekle
            var dosyaAdi = odev.FilePath.Trim();
            var tamDosyaYolu = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "odevler", dosyaAdi);
            
            if (!System.IO.File.Exists(tamDosyaYolu))
            {
                return NotFound($"Dosya bulunamadı: {tamDosyaYolu}");
            }

            var dosyaBytes = System.IO.File.ReadAllBytes(tamDosyaYolu);
            
            return File(dosyaBytes, "application/octet-stream", dosyaAdi);
        }
    }
}
