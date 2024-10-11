using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tickets.DLL.Models;

namespace Tickets.BLL.Interfaces
{
    public interface IEventUserRepository : IGenericRepository<UserEvent>
    {
        IEnumerable<Event> GetEventByUserId(string id);
     
    }
}
