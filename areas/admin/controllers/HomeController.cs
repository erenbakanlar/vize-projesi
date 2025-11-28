using Microsoft.AspNetCore.Mvc;

namespace odev.dagitim.portali.areas.admin.controllers
{
    [Area("Admin")]
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
