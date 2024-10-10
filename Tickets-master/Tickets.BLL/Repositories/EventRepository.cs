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
    public class EventRepository : GenericRepository<Event>, IEventRepository
    {
        public EventRepository(TicketsDbContext _context) : base(_context)
        {
        }

        public IEnumerable<Event> GetAllPending()
        {
            return context.Set<Event>().Where(e => !e.IsApproved && !e.IsRejected).ToList();
        }
        public IEnumerable<Event> GetAllApprovedEvents()
        {
            return context.Set<Event>().Where(e => e.IsApproved).ToList();
            //return new List<Event>();
        }
        public void ApproveEvent(int eventId)
        {
            var eventToApprove = context.Set<Event>().Find(eventId);
            if (eventToApprove != null)
            {
                eventToApprove.IsApproved = true;
                Save();
            }
        }

        public void RejectEvent(int eventId)
        {
            var eventToReject = context.Set<Event>().Find(eventId);
            if (eventToReject != null)
            {
                //Remove(eventToReject);
                eventToReject.IsRejected = true;
                Save();
            }
        }

        public IEnumerable<Event> SearchEvents(string searchTerm)
        {
            if (string.IsNullOrEmpty(searchTerm))
            {
                return GetAllApprovedEvents(); // Return all if no search term
            }

            var lowerSearchTerm = searchTerm.ToLower();

            return GetAllApprovedEvents()
                .Where(e => e.Name.ToLower().Contains(lowerSearchTerm) ||
                            e.Description.ToLower().Contains(lowerSearchTerm))
                .ToList();
        }
        }
    }

