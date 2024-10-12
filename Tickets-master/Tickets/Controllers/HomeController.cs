using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Formats.Asn1;
using Tickets.BLL.Interfaces;
using Tickets.BLL.Repositories;
using Tickets.DLL.Models;
using Tickets.Models;

namespace Tickets.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        private readonly IEventRepository eventRepository;
        private readonly UserManager<IdentityUser> userManager;

        public HomeController(ILogger<HomeController> logger , IEventRepository _eventRepository, UserManager<IdentityUser> userManager)
        {
            eventRepository = _eventRepository;
            this.userManager = userManager;
            _logger = logger;
        }

        public async Task<IActionResult> Index()
        {

          //  var approvedEvents = eventRepository.GetAllApprovedEvents();
            return RedirectToAction("SearchEvent","Event");
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
       
    }
}