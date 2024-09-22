using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tickets.DLL.Models;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace Tickets.DLL.Context
{
    public class TicketsDbContext : IdentityDbContext<IdentityUser>
    {
        public TicketsDbContext(DbContextOptions<TicketsDbContext> options) : base(options)
        {
            
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=.;Database=Tickets; Encrypt=false ; Integrated Security=True", b => b.MigrationsAssembly("Tickets"));
            base.OnConfiguring(optionsBuilder);
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Organizer> Organizers { get; set; }
        public DbSet<Event> Events { get; set; }
    }
}
