using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tickets.BLL.Interfaces;

namespace Tickets.BLL.Repositories
{
    public class UintOfWork : IUintOfWork
    {
        public UintOfWork(IOrganizrRepository _OrganizrRepository)
        {
            OrganizrRepository = _OrganizrRepository;
        }
        public IOrganizrRepository OrganizrRepository { get; set; }
    }
}
