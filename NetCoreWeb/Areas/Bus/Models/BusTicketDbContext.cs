using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
namespace NetCoreWeb.Areas.Bus.Models
{
    public class BusTicketDbContext : DbContext
    {
        public BusTicketDbContext(DbContextOptions<BusTicketDbContext> options) : base(options) { }
        public DbSet<Ticket> Tickets { get; set; }
        public DbSet<TicketOrder> TicketOrders { get; set; }
    }
}
