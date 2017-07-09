using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NetCoreWeb.Areas.Bus.Models
{
    //车票“购物车”
    public class TicketCart
    {
        private List<TicketCartLine> lineCollection = new List<TicketCartLine>();
        public virtual void AddItem(Ticket ticket, int quantity)
        {
            Clear();//每次只能选一张票
            TicketCartLine line = lineCollection.Where(t => t.Ticket.TicketID == ticket.TicketID).FirstOrDefault();
            if (line == null)
            {
                lineCollection.Add(new TicketCartLine { Ticket = ticket, Quantity = quantity });
            }
            else
            {
                line.Quantity += quantity;
            }
        }
        public virtual void RemoveLine(Ticket ticket) => lineCollection.RemoveAll(l => l.Ticket.TicketID == ticket.TicketID);
        public virtual decimal ComputeTotalValue() => lineCollection.Sum(e => e.Ticket.Price * e.Quantity);
        public virtual void Clear() => lineCollection.Clear();
        public virtual IEnumerable<TicketCartLine> Lines => lineCollection;
    }
    public class TicketCartLine
    {
        public int TicketCartLineID { get; set; }
        public Ticket Ticket { get; set; }
        //购票数量
        public int Quantity { get; set; }
    }
}
