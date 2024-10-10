using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tickets.DLL.Models;

namespace Tickets.BLL.Interfaces
{
    public interface IEventRepository : IGenericRepository<Event>
    {
        IEnumerable<Event> GetAllPending();
        IEnumerable<Event> GetAllApprovedEvents();
        void ApproveEvent(int eventId);
        void RejectEvent(int eventId);
        IEnumerable<Event> SearchEvents(string searchTerm);
}
}
