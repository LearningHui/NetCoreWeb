using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NetCoreWeb.Areas.Bus.Models.ViewModels
{
    public class TicketCartIndexViewModel
    {
        public TicketCart Cart { get; set; }
        public string ReturnUrl { get; set; }
    }
}
