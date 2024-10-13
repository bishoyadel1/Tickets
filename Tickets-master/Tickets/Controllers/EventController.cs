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

        public async Task<IActionResult> EventDetails(int Id)
        {
            var DesiredEvent = eventRepo.Get(Id).Result;
            var EventOrgnaizer = await userManager.FindByIdAsync(DesiredEvent.OrganizerId);
            string organizerName = EventOrgnaizer.UserName;

            EventOrgnaiserViewModel eventOrgnaiserViewModel = new EventOrgnaiserViewModel()
            {
                EventId = Id,
                OrganizerName = organizerName,
                EventName =    DesiredEvent.Name,
                Date = DesiredEvent.Date,
                Description = DesiredEvent.Description,
                TotalNumberOfTickets = DesiredEvent.TotalNumberOfTickets,
                Image = DesiredEvent.Image,
                ImageUrl = DesiredEvent.ImageUrl,
                Price= DesiredEvent.Price,
            };
            

            return View(eventOrgnaiserViewModel);
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
                    return RedirectToAction("GetBookedEvents");
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
                objEvent.ImageUrl = ImageConfig.ImageSetting.UploadImage(objEvent.Image);
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




        public IActionResult SearchEvent(string searchTerm, string sortBy = "Date", bool isAscending = false, int page = 1, int pageSize = 3)
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
                    events = events.OrderByDescending(e => e.Date).ToList();
                    break;
            }

            // Pagination logic
            int totalEvents = events.Count();
            events = events.Skip((page - 1) * pageSize).Take(pageSize).ToList();

            // Pass data to the view
            ViewData["SearchTerm"] = searchTerm;
            ViewData["SortBy"] = sortBy;
            ViewData["IsAscending"] = isAscending;
            ViewData["CurrentPage"] = page;
            ViewData["TotalPages"] = (int)Math.Ceiling((double)totalEvents / pageSize);

            return View("~/Views/Home/Index.cshtml", events);
        }


    }
}
