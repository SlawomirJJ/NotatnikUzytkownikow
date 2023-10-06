using Microsoft.AspNetCore.Mvc;

namespace NotatnikUzytkownikow.Controllers
{
    public class NotatnikController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
