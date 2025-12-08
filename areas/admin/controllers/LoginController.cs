using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace odev.dagitim.portali.Areas.Admin.Controllers
{
    [Area("Admin")]
    [AllowAnonymous]   
    public class LoginController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> Giris(string kullanici, string sifre)
        {
            if (kullanici == "admin" && sifre == "1234")
            {
                var claims = new List<Claim>
        {
            new Claim(ClaimTypes.Name, kullanici),
            new Claim(ClaimTypes.Role, "Admin")
        };

                var identity = new ClaimsIdentity(claims, "MyCookieAuth");
                var principal = new ClaimsPrincipal(identity);

                await HttpContext.SignInAsync("MyCookieAuth", principal);


                return RedirectToAction("Listele", "DagitilanOdev", new { area = "Admin" });

            }

            ViewBag.Hata = "Kullanıcı adı veya şifre yanlış!";
            return View("Index");
        }


        public async Task<IActionResult> Cikis()
        {
            await HttpContext.SignOutAsync("MyCookieAuth");
            return RedirectToAction("Index");
        }

    }
}
