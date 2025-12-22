// ARTIK KULLANILMIYOR

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using odev.dagitim.portali.data;
using System.Linq;

namespace odev.dagitim.portali.Areas.User.Controllers
{
    [Area("User")]
    [Authorize]
    [Obsolete("ARTIK KULLANILMIYOR - Identity kullanılıyor")]
    public class LoginController : Controller
    {
        private readonly AppDbContext _context;

        public LoginController(AppDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return Redirect("/Identity/Account/Login");
        }
        [HttpPost]
        public IActionResult Index(string Numara, string Sifre)
        {
            return Redirect("/Identity/Account/Login");
        }  
        public IActionResult Cikis()
        {
            HttpContext.Session.Clear(); 
            return RedirectToAction("Index", "Home", new { area = "" }); 
        }

    }
}
