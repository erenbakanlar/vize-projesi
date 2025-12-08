using Microsoft.AspNetCore.Mvc;
using odev.dagitim.portali.repositories;

namespace odev.dagitim.portali.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class OdevController : Controller
    {
        private readonly IOdevRepository _odevRepository;

        public OdevController(IOdevRepository odevRepository)
        {
            _odevRepository = odevRepository;
        }

        public IActionResult Listele()
        {
            var odevler = _odevRepository.TumunuGetir();
            return View(odevler);
        }
    }
}
