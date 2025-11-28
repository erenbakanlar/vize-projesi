using Microsoft.AspNetCore.Mvc;

namespace odev.dagitim.portali.areas.user.controllers
{
    [Area("user")]
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
