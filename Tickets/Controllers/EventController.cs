using Microsoft.AspNetCore.Mvc;

namespace Tickets.Controllers
{
    public class EventController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
