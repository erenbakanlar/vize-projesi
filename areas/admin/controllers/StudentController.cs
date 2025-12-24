using Microsoft.AspNetCore.Authorization;
using odev.dagitim.portali.repositories;
using Microsoft.AspNetCore.Mvc;
using odev.dagitim.portali.data;
using odev.dagitim.portali.models;

namespace odev.dagitim.portali.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class StudentController : Controller
    {
        private readonly IStudentRepository _ogrenciRepository;

        public StudentController(IStudentRepository ogrenciRepository)
        {
            _ogrenciRepository = ogrenciRepository;
        }

        public IActionResult Ekle()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Ekle(Student ogrenci)
        {
            _ogrenciRepository.Ekle(ogrenci);
            _ogrenciRepository.Kaydet();

            return RedirectToAction("Listele");
        }

        public IActionResult Listele()
        {
            var ogrenciler = _ogrenciRepository.TumunuGetir();
            return View(ogrenciler);
        }

        [HttpPost]
        public IActionResult AjaxEkle(Student ogrenci)
        {
            _ogrenciRepository.Ekle(ogrenci);
            _ogrenciRepository.Kaydet();

            return Json(new
            {
                success = true,
                id = ogrenci.Id,
                adSoyad = ogrenci.FullName,
                numara = ogrenci.Email
            });
        }

        [HttpPost]
        public IActionResult Sil(int id)
        {
            _ogrenciRepository.Sil(id);
            _ogrenciRepository.Kaydet();
            return Json(new { success = true });
        }

    }
}
