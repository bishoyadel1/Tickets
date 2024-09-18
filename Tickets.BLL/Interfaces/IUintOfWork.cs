using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tickets.BLL.Interfaces
{
    public interface IUintOfWork
    {
        public IOrganizrRepository OrganizrRepository { get; set; }
    }
}
