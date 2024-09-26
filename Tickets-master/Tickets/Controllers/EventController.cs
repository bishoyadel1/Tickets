using Microsoft.AspNetCore.Mvc;
using Tickets.BLL.Interfaces;
using Tickets.BLL.Repositories;
using Tickets.DLL.Context;
using Tickets.DLL.Models;
namespace Tickets.Controllers
{
    public class EventController : Controller
    {
        private readonly IGenericRepository<Event> eventRepo;
        private readonly IEventRepository eventRepository;

        public EventController(IGenericRepository<Event> _eventRepo, IEventRepository _eventRepository)
        {
            eventRepo = _eventRepo;
            eventRepository = _eventRepository;
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult EventDetails(int Id)
        {

            var DesiredEvent = eventRepo.Get(Id).Result;

            return View(DesiredEvent);
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
            var unapprovedEvents = eventRepository.GetAllPending();
            return View(unapprovedEvents);
        }

        public IActionResult ApprovedEvents()
        {
            var approvedEvents = eventRepository.GetAllApprovedEvents();
            return View(approvedEvents);
        }
        public IActionResult Approve(int eventId)
        {
            eventRepository.ApproveEvent(eventId);
            return RedirectToAction("EventRequests");
        }

        public IActionResult RejectEvent(int eventId)
        {
            eventRepository.RejectEvent(eventId);
            return RedirectToAction("EventRequests");
        }

    }
}
