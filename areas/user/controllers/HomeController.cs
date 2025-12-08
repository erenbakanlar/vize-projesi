using Microsoft.AspNetCore.Mvc;

[Area("User")]
public class HomeController : Controller
{
    public IActionResult Index()
    {
        return RedirectToAction("Index", "Panel", new { area = "User" });
    }
}
