using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;
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
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        public readonly TicketsDbContext context;

        public GenericRepository(TicketsDbContext _context)
        {
            this.context = _context;
        }

        public async Task<int> Add(T ob)
        {
            await context.Set<T>().AddAsync(ob);
            return context.SaveChanges();
        }

        public async Task<T> Get(int? Id)
        => await context.Set<T>().FindAsync(Id);

        public async Task<List<T>> GetAll()
        =>await context.Set<T>().ToListAsync();

        public async Task<int> Remove(T ob)
        {
             context.Set<T>().Remove(ob);
            return context.SaveChanges();
        }
    

        public async Task<int> Update(T ob)
        {
            context.Set<T>().Update(ob);
            return context.SaveChanges();
        }

        public void Save()
        {
            context.SaveChanges();
        }

    }
}
