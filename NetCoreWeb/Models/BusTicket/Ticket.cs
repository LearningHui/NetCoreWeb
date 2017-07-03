using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace NetCoreWeb.Models.BusTicket
{
    public class Ticket
    {
        public int TicketID { get; set; }
        [Required(ErrorMessage = "请输入起始站")]
        public string StartStation { get; set; }
        [Required(ErrorMessage = "请输入终点站")]
        public string TerminalStation { get; set; }        
        [Required]
        [Range(0.01, double.MaxValue, ErrorMessage = "请输入票价")]
        public decimal Price { get; set; }
        [Required(ErrorMessage = "请输入描述信息")]
        public string Description { get; set; }
        [Required(ErrorMessage = "请输入所属车次类别")]
        public string Category { get; set; }
    }
}
