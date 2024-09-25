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

        [HttpGet]
        public IActionResult AddEvent()
        {
            var Eevent = new Event();
            return View(Eevent);
        }
        [HttpPost]
        public IActionResult SaveEvent(Event objEvent)
        {
            if (objEvent.Name != null &&
                objEvent.TotalNumberOfTicekts != null &&
                objEvent.Description != null &&
                objEvent.Image != null && objEvent.Date != null)
            {
                eventRepo.Add(objEvent);
                return RedirectToAction("Index");
            }
            return View("AddEvent");
        }
        public IActionResult EventRequests()
        {
            return View();
        }



    }
}
