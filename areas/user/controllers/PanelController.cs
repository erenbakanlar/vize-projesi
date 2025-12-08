using Microsoft.AspNetCore.Mvc;

namespace odev.dagitim.portali.Areas.User.Controllers
{
    [Area("User")]
    public class PanelController : Controller
    {
        public IActionResult Index()
        {
            var ogrenciId = HttpContext.Session.GetInt32("OgrenciId");

            if (ogrenciId == null)
            {
                return RedirectToAction("Index", "Login", new { area = "User" });

            }

            ViewBag.OgrenciAd = HttpContext.Session.GetString("OgrenciAd");
            return View();
        }
    }
}
