using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using odev.dagitim.portali.data;

namespace odev.dagitim.portali.Areas.Admin.Controllers
{
    // [VIDEO - ROL YÖNETİMİ - Admin Authorization BAŞLANGIÇ]
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class HomeController : Controller
    {
        private readonly AppDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;

        public HomeController(AppDbContext context, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public IActionResult Index()
        {
            // Direkt ödev listesine yönlendir
            return RedirectToAction("Listele", "AssignedHomework");
        }

        public async Task<IActionResult> Profil()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return Redirect("/Identity/Account/AdminLogin");
            }

            // İstatistikler
            ViewBag.Email = user.Email;
            ViewBag.ToplamOgrenci = _context.Ogrenciler.Count();
            ViewBag.ToplamDagitilanOdev = _context.DagitilanOdevler.Count();
            ViewBag.ToplamYuklenenOdev = _context.Odevler.Count(o => o.StudentId > 0);
            ViewBag.BuAyYuklenen = _context.Odevler.Count(o => 
                o.UploadDate.Month == DateTime.Now.Month && 
                o.UploadDate.Year == DateTime.Now.Year);

            return View();
        }

    }
    // [VIDEO - ROL YÖNETİMİ - Admin Authorization BİTİŞ]
}
