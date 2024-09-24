using Microsoft.AspNetCore.Mvc;
using Tickets.BLL.Interfaces;
using Tickets.DLL.Models;

namespace Tickets.Controllers
{
    public class EventController : Controller
    {
        private readonly IGenericRepository<Event> eventRepo;

        public EventController(IGenericRepository<Event> _eventRepo)
        {
            eventRepo = _eventRepo;
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult AddEvent()
        {
            return View();
        }
        public IActionResult SaveEvent(Event objEvent)
        {
            if (ModelState.IsValid)
            {
                eventRepo.Add(objEvent);
                return RedirectToAction("Index");
            }
            else
            {
                return View(objEvent);
            }
        }
        public IActionResult EventRequests()
        {
            return View();
        }



    }
}
