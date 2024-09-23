using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tickets.BLL.Repositories;
using Tickets.DLL.Models;

namespace Tickets.BLL.Interfaces
{
    public interface IOrganizrRepository : IGenericRepository<IdentityUser>
    {
    }
}
