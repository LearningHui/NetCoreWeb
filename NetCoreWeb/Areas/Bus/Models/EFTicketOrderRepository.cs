using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace NetCoreWeb.Areas.Bus.Models
{
    public class EFTicketOrderRepository : ITicketOrderRepository
    {
        private BusTicketDbContext context;
        public EFTicketOrderRepository(BusTicketDbContext ctx)
        {
            context = ctx;
        }
        public IEnumerable<TicketOrder> Orders =>
            context.TicketOrders.Include(o => o.Lines).ThenInclude(l => l.Ticket);
        public void SaveOrder(TicketOrder order)
        {
            context.AttachRange(order.Lines.Select(l => l.Ticket));
            if (order.TicketOrderID == 0)
            {
                context.TicketOrders.Add(order);
            }
            context.SaveChanges();
        }
    }
}
