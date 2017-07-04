using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NetCoreWeb.Areas.Bus.Models
{
    public class EFTicketRepository : ITicketRepository
    {
        private BusTicketDbContext context;
        public EFTicketRepository(BusTicketDbContext ctx) { context = ctx; }
        public IEnumerable<Ticket> Tickets => context.Tickets;
        public void SaveProduct(Ticket ticket)
        {
            if (ticket.TicketID == 0)
            {
                context.Tickets.Add(ticket);
            }
            else
            {
                Ticket dbEntry = context.Tickets.FirstOrDefault(t => t.TicketID == ticket.TicketID);
                if (dbEntry != null)
                {
                    dbEntry.StartStation = ticket.StartStation;
                    dbEntry.TerminalStation = ticket.TerminalStation;
                    dbEntry.Description = ticket.Description;
                    dbEntry.Price = ticket.Price;
                    dbEntry.Category = ticket.Category;
                }
            }
            context.SaveChanges();
        }
        public Ticket DeleteTicket(int ticketID)
        {
            Ticket dbEntry = context.Tickets.FirstOrDefault(t => t.TicketID == ticketID);
            if (dbEntry != null)
            {
                context.Tickets.Remove(dbEntry);
                context.SaveChanges();
            }
            return dbEntry;
        }
    }
}
