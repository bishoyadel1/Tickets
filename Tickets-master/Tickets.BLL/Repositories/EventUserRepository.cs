using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tickets.BLL.Interfaces;
using Tickets.DLL.Context;
using Tickets.DLL.Models;

namespace Tickets.BLL.Repositories
{
    public class EventUserRepository : GenericRepository<UserEvent>, IEventUserRepository
    {
        public EventUserRepository(TicketsDbContext _context) : base(_context)
        {
        }

        public IEnumerable<Event> GetEventByUserId(string id)
        {
            //var UserEvent = context.UserEvent.Where(i => i.UserId == id).Include(i => i.Event).ToList();

          
       
                var userEvents = context.UserEvent
                                        .Where(ue => ue.UserId == id)
                                        .Include(ue => ue.Event)
                                        .Select(ue => ue.Event)
                                        .ToList();

                return userEvents;
         

        }
    }
}
