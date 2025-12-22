using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using odev.dagitim.portali.repositories;

namespace odev.dagitim.portali.Areas.User.Controllers
{
    [Area("User")]
    [Authorize]
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
    }
}
