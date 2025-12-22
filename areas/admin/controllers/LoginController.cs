// NOT: Bu controller vizede kullanılan manuel cookie authentication içindi.
// Finalde ASP.NET Identity kullanıldığı için DEVRE DIŞI BIRAKILMIŞTIR.
// Login işlemleri /Identity/Account/Login üzerinden yapılmaktadır.

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace odev.dagitim.portali.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Obsolete("ARTIK KULLANILMIYOR - Identity kullanılıyor")]
    [AllowAnonymous]
    public class LoginController : Controller
    {
       
        public IActionResult Index()
        {
            return Redirect("/Identity/Account/Login");
        }

        
        [HttpPost]
        public IActionResult Giris()
        {
            return Redirect("/Identity/Account/Login");
        }
    }
}

