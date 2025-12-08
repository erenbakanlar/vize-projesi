using odev.dagitim.portali.repositories;
using Microsoft.AspNetCore.Mvc;
using odev.dagitim.portali.data;
using odev.dagitim.portali.models;

namespace odev.dagitim.portali.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class OgrenciController : Controller
    {
        private readonly IOgrenciRepository _ogrenciRepository;

        public OgrenciController(IOgrenciRepository ogrenciRepository)
        {
            _ogrenciRepository = ogrenciRepository;
        }

        public IActionResult Ekle()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Ekle(Ogrenci ogrenci)
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
        public IActionResult AjaxEkle(Ogrenci ogrenci)
        {
            _ogrenciRepository.Ekle(ogrenci);
            _ogrenciRepository.Kaydet();

            return Json(new
            {
                success = true,
                id = ogrenci.Id,
                adSoyad = ogrenci.AdSoyad,
                numara = ogrenci.Numara
            });
        }

    }
}
