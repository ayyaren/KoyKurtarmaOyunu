using Microsoft.AspNetCore.Mvc;

namespace KoyKurtarmaOyunu.Controllers
{
    public class TestController : Controller
    {
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Submit()
        {
            return Content("POST isteği başarılı!");
        }
    }
}
