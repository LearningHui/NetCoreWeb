using NetCoreWeb.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NetCoreWeb.Areas.Bus.Models.ViewModels
{
    public class TicketsListViewModel
    {
        public IEnumerable<Ticket> Tickets { get; set; }
        public PagingInfo PagingInfo { get; set; }
        public string CurrentCategory { get; set; }
    }
}
