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
    public class OrganizrRepository : GenericRepository<Organizer>, IOrganizrRepository
    {
        public OrganizrRepository(TicketsDbContext _context) : base(_context)
        {
        }
    }
}
