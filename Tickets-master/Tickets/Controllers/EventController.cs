using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Net.Sockets;
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
        private readonly IEventUserRepository eventUserRepository;

        public EventController(IGenericRepository<Event> _eventRepo, IEventRepository _eventRepository, UserManager<IdentityUser> userManager , IEventUserRepository _eventUserRepository)
        {
            eventRepo = _eventRepo;
            eventRepository = _eventRepository;
            this.userManager = userManager;
            eventUserRepository = _eventUserRepository;
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
        [Authorize]
        public async Task<IActionResult> BookTicket(int Id)
        {
            var user = await userManager.GetUserAsync(User);
            var CheckForTicket = await eventRepo.Get(Id);
            if (CheckForTicket != null)
            {
                if (CheckForTicket.TotalNumberOfTickets > 0)
                {
                    var UserEvent = new UserEvent { UserId = user.Id, EventId = CheckForTicket.Id };
                    CheckForTicket.TotalNumberOfTickets--;
                    await eventUserRepository.Add(UserEvent);
                    await eventRepository.Update(CheckForTicket);
                    return RedirectToAction("Index", "Home");
                }
                else
                    return RedirectToAction("Index", "Home");



            }
            else 
                return NotFound();

          
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

        [Authorize]
        public async Task<IActionResult> GetBookedEvents()
        {

            var user = await userManager.GetUserAsync(User);

            var Event = eventUserRepository.GetEventByUserId(user.Id);

            return View(Event);

        }
        [Authorize(Roles = "Admin,Organizer")]
        public async Task<IActionResult> GetTicketUploaded()
        {

            var user = await userManager.GetUserAsync(User);

            if(user != null)
            { var events =  eventRepository.GetByOrganizerId(user.Id);
                return View(events);
            }

            return NotFound();

        }


        public IActionResult SearchEvent(string searchTerm, string sortBy, bool isAscending = true)
        {

            var events = eventRepository.SearchEvents(searchTerm);

            return View("~/Views/Home/Index.cshtml", events);
        }

    }
}
