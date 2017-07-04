using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using NetCoreWeb.Areas.Bus.Models;
using NetCoreWeb.Areas.Bus.Models.ViewModels;

namespace NetCoreWeb.Areas.Bus.Controllers
{
    [Area("Bus")]
    public class CartController : Controller
    {
        private ITicketRepository repository;
        private TicketCart cart;

        public CartController(ITicketRepository repo, TicketCart cartService)
        {
            repository = repo;
            cart = cartService;
        }
        public ViewResult Index(string returnUrl)
        {
            return View(new TicketCartIndexViewModel
            {
                Cart = cart,
                ReturnUrl = returnUrl
            });
        }
        public RedirectToActionResult AddToCart(int ticketId, string returnUrl)
        {
            Ticket ticket = repository.Tickets.FirstOrDefault(p => p.TicketID == ticketId);
            if (ticket != null)
            {
                cart.AddItem(ticket, 1);
            }
            return RedirectToAction("Index", new { returnUrl });
        }
        public RedirectToActionResult RemoveFromCart(int ticketId, string returnUrl)
        {
            Ticket ticket = repository.Tickets.FirstOrDefault(t => t.TicketID == ticketId);
            if (ticket != null)
            {
                cart.RemoveLine(ticket);
            }
            return RedirectToAction("Index", new { returnUrl });
        }
    }
}