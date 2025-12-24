using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using odev.dagitim.portali.repositories;

namespace odev.dagitim.portali.Areas.User.Controllers
{
    [Area("User")]
    [Authorize]
    public class AssignedHomeworkController : Controller
    {
        private readonly IAssignedHomeworkRepository _repository;

        public AssignedHomeworkController(IAssignedHomeworkRepository repository)
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
