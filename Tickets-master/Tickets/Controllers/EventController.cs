using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Tickets.BLL.Interfaces;
using Tickets.BLL.Repositories;
using Tickets.DLL.Context;
using Tickets.DLL.Models;
using Tickets.Models;
namespace Tickets.Controllers
{
    public class EventController : Controller
    {
        private readonly IGenericRepository<Event> eventRepo;
        private readonly IEventRepository eventRepository;
        private readonly UserManager<IdentityUser> userManager;

        public EventController(IGenericRepository<Event> _eventRepo, IEventRepository _eventRepository, UserManager<IdentityUser> userManager)
        {
            eventRepo = _eventRepo;
            eventRepository = _eventRepository;
            this.userManager = userManager;
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

        [Authorize(Roles = "Admin,Organizer")]
        [HttpGet]
        public IActionResult AddEvent()
        {
            var Eevent = new Event();
            return View(Eevent);
        }
        [Authorize(Roles = "Admin,Organizer")]
        [HttpPost]
        public async Task<IActionResult> SaveEvent(Event objEvent)
        {
            if (objEvent.Name != null &&
                objEvent.TotalNumberOfTickets != null &&
                objEvent.Description != null &&
                objEvent.Image != null && objEvent.Date != null)
                
            {
                var user = await userManager.GetUserAsync(User);
                objEvent.OrganizerId = user.Id;
                eventRepo.Add(objEvent);
                return RedirectToAction("Index");
            }
            return View("AddEvent");
        }

        [Authorize(Roles = "Admin")]
        public IActionResult EventRequests()
        {
            var unapprovedEvents = eventRepository.GetAllPending();
            return View(unapprovedEvents);
        }


        [Authorize(Roles = "Admin")]
        public IActionResult ApprovedEvents()
        {
            var approvedEvents = eventRepository.GetAllApprovedEvents();
            return View(approvedEvents);
        }

        [Authorize(Roles = "Admin")]
        public IActionResult Approve(int eventId)
        {
            eventRepository.ApproveEvent(eventId);
            return RedirectToAction("EventRequests");
        }

        [Authorize(Roles = "Admin")]
        public IActionResult RejectEvent(int eventId)
        {
            eventRepository.RejectEvent(eventId);
            return RedirectToAction("EventRequests");
        }

      
        public IActionResult SearchEvent(string searchTerm, string sortBy = "Date", bool isAscending = false)
        {
            // Fetch events based on search term
            var events = eventRepository.SearchEvents(searchTerm);

            // Sorting logic for descending by default
            switch (sortBy)
            {
                case "Date":
                    events = isAscending ? events.OrderBy(e => e.Date).ToList() : events.OrderByDescending(e => e.Date).ToList();
                    break;
                case "Name":
                    events = isAscending ? events.OrderBy(e => e.Name).ToList() : events.OrderByDescending(e => e.Name).ToList();
                    break;
                default:
                    // Default sorting (by Date in descending order)
                    events = events.OrderByDescending(e => e.Date).ToList();
                    break;
            }

            // Pass search term and sort parameters to the view
            ViewData["SearchTerm"] = searchTerm;
            ViewData["SortBy"] = sortBy;
            ViewData["IsAscending"] = isAscending;

            return View("~/Views/Home/Index.cshtml", events);
        }


    }
}
