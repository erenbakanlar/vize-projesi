using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using odev.dagitim.portali.repositories;
using odev.dagitim.portali.viewmodels;

namespace odev.dagitim.portali.Areas.User.Controllers
{
    [Area("User")]
    [Authorize]
    public class PanelController : Controller
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IStudentRepository _ogrenciRepository;
        private readonly IAssignedHomeworkRepository _dagitilanOdevRepository;
        private readonly IHomeworkRepository _odevRepository;

        public PanelController(
            UserManager<IdentityUser> userManager,
            IStudentRepository ogrenciRepository,
            IAssignedHomeworkRepository dagitilanOdevRepository,
            IHomeworkRepository odevRepository)
        {
            _userManager = userManager;
            _ogrenciRepository = ogrenciRepository;
            _dagitilanOdevRepository = dagitilanOdevRepository;
            _odevRepository = odevRepository;
        }

        // [VIDEO - ÖĞRENCİ PANELİ - Dashboard BAŞLANGIÇ]
        public async Task<IActionResult> Index()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return Redirect("/Identity/Account/Login");
            }

            // Öğrenci kaydı kontrolü - sadece admin tarafından eklenmiş emailler
            var ogrenci = _ogrenciRepository.NumarayaGoreGetir(user.Email);
            if (ogrenci == null)
            {
                TempData["Error"] = "Erişim reddedildi! Öğrenci kaydınız bulunamadı. Lütfen yöneticinizle iletişime geçin.";
                return Redirect("/Identity/Account/AccessDenied");
            }

            // Dağıtılan ödevleri ve yüklenenleri getir
            var dagitilanOdevler = _dagitilanOdevRepository.GetAll();
            var yuklenmiOdevler = _odevRepository.OgrenciyeGoreGetir(ogrenci.Id);

            var viewModel = dagitilanOdevler.Select(d => new UserOdevViewModel
            {
                DagitilanOdev = d,
                YuklenenOdev = yuklenmiOdevler.FirstOrDefault(y => y.AssignedHomeworkId == d.Id)
            })
            .OrderByDescending(x => x.YuklenenOdev != null)
            .ThenByDescending(x => x.YuklenenOdev?.UploadDate)
            .ThenBy(x => x.DagitilanOdev.DueDate)
            .ToList();

            return View(viewModel);
        }
        // [VIDEO - ÖĞRENCİ PANELİ - Dashboard BİTİŞ]

        // [VIDEO - ÖDEV YÜKLEME - GET Action BAŞLANGIÇ]
        [HttpGet]
        public async Task<IActionResult> Yukle(int id)
        {
            Console.WriteLine($"🔴 YUKLE GET - ID: {id}");
            
            var user = await _userManager.GetUserAsync(User);
            Console.WriteLine($"🔴 USER: {user?.Email ?? "NULL"}");
            
            if (user == null)
            {
                return Redirect("/Identity/Account/Login");
            }

            // Öğrenci kaydı kontrolü
            var ogrenci = _ogrenciRepository.NumarayaGoreGetir(user.Email);
            Console.WriteLine($"🔴 OGRENCI: {ogrenci?.Id ?? 0}");
            
            if (ogrenci == null)
            {
                TempData["Error"] = "Erişim reddedildi! Öğrenci kaydınız bulunamadı.";
                return RedirectToAction("Index");
            }

            var dagitilanOdev = _dagitilanOdevRepository.GetAll().FirstOrDefault(d => d.Id == id);
            Console.WriteLine($"🔴 DAGITILAN ODEV: {dagitilanOdev?.Title ?? "NULL"}");
            
            if (dagitilanOdev == null)
            {
                return NotFound();
            }

            Console.WriteLine($"🔴 VIEW DONULUYOR!");
            return View(dagitilanOdev);
        }
        // [VIDEO - ÖDEV YÜKLEME - GET Action BİTİŞ]

        // [VIDEO - DOSYA UPLOAD - POST Action BAŞLANGIÇ]
        [HttpPost]
        public async Task<IActionResult> Yukle(int id, IFormFile dosya)
        {
            if (dosya == null || dosya.Length == 0)
            {
                ModelState.AddModelError("", "Lütfen bir dosya seçin.");
                return View(_dagitilanOdevRepository.GetAll().FirstOrDefault(d => d.Id == id));
            }

            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return Redirect("/Identity/Account/Login");
            }

            // Öğrenci kaydı kontrolü
            var userEmail = user.Email;
            Console.WriteLine($"[DEBUG] Login Email: {userEmail}");
            
            var ogrenci = _ogrenciRepository.NumarayaGoreGetir(userEmail);
            
            if (ogrenci == null)
            {
                Console.WriteLine($"[DEBUG] Öğrenci BULUNAMADI! Email: {userEmail}");
                TempData["Error"] = $"HATA! Identity Email: '{userEmail}' - Öğrenci kaydınız bulunamadı. SQL'de kontrol edin!";
                return RedirectToAction("Index");
            }
            
            Console.WriteLine($"[DEBUG] Öğrenci BULUNDU! Id: {ogrenci.Id}, Email: {ogrenci.Email}");

            // Dosya kayıt işlemi
            var odevKlasoru = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "odevler");
            if (!Directory.Exists(odevKlasoru))
            {
                Directory.CreateDirectory(odevKlasoru);
            }

            var dosyaAdi = $"{Guid.NewGuid()}{Path.GetExtension(dosya.FileName)}";
            var dosyaYolu = Path.Combine(odevKlasoru, dosyaAdi);

            using (var stream = new FileStream(dosyaYolu, FileMode.Create))
            {
                await dosya.CopyToAsync(stream);
            }

            // Veritabanı kayıt işlemi
            var existingOdev = _odevRepository.OgrenciyeGoreGetir(ogrenci.Id)
                .FirstOrDefault(o => o.AssignedHomeworkId == id);

            if (existingOdev != null)
            {
                // Eski dosyayı sil
                var eskiDosyaYolu = Path.Combine(odevKlasoru, existingOdev.FilePath.Trim());
                if (System.IO.File.Exists(eskiDosyaYolu))
                {
                    System.IO.File.Delete(eskiDosyaYolu);
                }

                existingOdev.FilePath = dosyaAdi;
                existingOdev.UploadDate = DateTime.Now;
                _odevRepository.Kaydet();
            }
            else
            {
                var yeniOdev = new odev.dagitim.portali.models.Homework
                {
                    StudentId = ogrenci.Id,
                    AssignedHomeworkId = id,
                    FilePath = dosyaAdi,
                    UploadDate = DateTime.Now
                };
                _odevRepository.Ekle(yeniOdev);
                _odevRepository.Kaydet();
            }

            TempData["Success"] = $"Ödev başarıyla yüklendi. Öğrenci ID: {ogrenci.Id}, Email: {ogrenci.Email}";
            return RedirectToAction("Index");
        }
        // [VIDEO - DOSYA UPLOAD - POST Action BİTİŞ]

        // [VIDEO - PROFİL SAYFASI - Action BAŞLANGIÇ]
        [HttpGet]
        public async Task<IActionResult> Profil()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return Redirect("/Identity/Account/Login");
            }

            var ogrenci = _ogrenciRepository.NumarayaGoreGetir(user.Email);
            if (ogrenci == null)
            {
                TempData["Error"] = "Öğrenci kaydınız bulunamadı.";
                return RedirectToAction("Index");
            }

            ViewBag.Email = user.Email;
            ViewBag.AdSoyad = ogrenci.FullName;
            ViewBag.OgrenciId = ogrenci.Id;

            return View();
        }
        // [VIDEO - PROFİL SAYFASI - Action BİTİŞ]
    }
}
