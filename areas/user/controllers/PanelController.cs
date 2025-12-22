using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

[Area("User")]
[Authorize]
public class PanelController : Controller
{
    public IActionResult Index()
    {
        return View();
    }
}
