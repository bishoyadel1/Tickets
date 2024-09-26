using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tickets.DLL.Models;

namespace Tickets.BLL.Interfaces
{
    public interface IGenericRepository<T> where T : class
    {
        public Task<T> Get(int? Id);
        
        public Task<List<T>> GetAll();
        public Task<int> Remove(T ob);

        public Task<int> Add(T ob);

        public Task<int> Update(T ob);
        void Save();



    }
}
