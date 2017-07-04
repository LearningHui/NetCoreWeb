using Microsoft.AspNetCore.Http;
using System;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NetCoreWeb.Infrastructure;
using Newtonsoft.Json;

namespace NetCoreWeb.Areas.Bus.Models
{
    public class SessionTicketCart : TicketCart
    {
        public static SessionTicketCart GetCart(IServiceProvider services)
        {
            ISession session = services.GetRequiredService<IHttpContextAccessor>()?.HttpContext.Session;
            SessionTicketCart cart = session?.GetJson<SessionTicketCart>("SessionTicketCart") ?? new SessionTicketCart();
            cart.Session = session;
            return cart;
        }
        [JsonIgnore]
        public ISession Session { get; set; }
        public override void AddItem(Ticket ticket, int quantity)
        {
            base.AddItem(ticket, quantity);
            Session.SetJson("SessionTicketCart", this);
        }
        public override void RemoveLine(Ticket ticket)
        {
            base.RemoveLine(ticket);
            Session.SetJson("SessionTicketCart", this);
        }
        public override void Clear()
        {
            base.Clear();
            Session.Remove("SessionTicketCart");
        }
    }
}
