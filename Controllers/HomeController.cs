using Microsoft.AspNetCore.Mvc;

namespace odev.dagitim.portali.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
