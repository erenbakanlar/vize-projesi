using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using odev.dagitim.portali.models;
using odev.dagitim.portali.repositories;

namespace odev.dagitim.portali.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class DagitilanOdevController : Controller
    {
        private readonly IDagitilanOdevRepository _repository;

        public DagitilanOdevController(IDagitilanOdevRepository repository)
        {
            _repository = repository;
        }

        public IActionResult Listele()
        {
            var odevler = _repository.GetAll();
            return View(odevler);
        }

        public IActionResult Ekle()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Ekle(DagitilanOdev odev)
        {
            _repository.Add(odev);
            return RedirectToAction("Listele");
        }
    }
}
