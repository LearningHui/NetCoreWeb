using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NetCoreWeb.Areas.Bus.Models
{
    public interface ITicketOrderRepository
    {
        IEnumerable<TicketOrder> Orders { get; }
        void SaveOrder(TicketOrder order);
    }
}
