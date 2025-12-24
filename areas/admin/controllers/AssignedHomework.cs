using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using odev.dagitim.portali.Hubs;
using odev.dagitim.portali.models;
using odev.dagitim.portali.repositories;

namespace odev.dagitim.portali.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class AssignedHomeworkController : Controller
    {
        private readonly IAssignedHomeworkRepository _repository;
        private readonly IHubContext<OdevHub> _hubContext;

        public AssignedHomeworkController(IAssignedHomeworkRepository repository, IHubContext<OdevHub> hubContext)
        {
            _repository = repository;
            _hubContext = hubContext;
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

        // [VIDEO - SIGNALR - Ödev Ekleme BAŞLANGIÇ]
        [HttpPost]
        public async Task<IActionResult> Ekle(AssignedHomework odev)
        {
            _repository.Add(odev);
            
           
            await _hubContext.Clients.All.SendAsync("OdevEklendi", odev.Title);
            
           
            await Task.Delay(1500);
            
           
            TempData["Success"] = "Ödev başarıyla eklendi!";
            
            return RedirectToAction("Listele");
        }
        // [VIDEO - SIGNALR - Ödev Ekleme BİTİŞ]

        [HttpPost]
        public IActionResult Sil(int id)
        {
            _repository.Delete(id);
            return Json(new { success = true });
        }
    }
}
