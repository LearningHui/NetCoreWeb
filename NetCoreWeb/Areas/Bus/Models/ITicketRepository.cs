using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NetCoreWeb.Areas.Bus.Models
{
    public interface ITicketRepository
    {
        IEnumerable<Ticket> Tickets { get; }
        void SaveProduct(Ticket ticket);
        Ticket DeleteTicket(int ticketID);
    }
}
