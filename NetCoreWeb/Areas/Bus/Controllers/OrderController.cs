using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using NetCoreWeb.Areas.Bus.Models;
using Microsoft.AspNetCore.Authorization;

namespace NetCoreWeb.Areas.Bus.Controllers
{
    [Area("Bus")]
    public class OrderController : Controller
    {
        private ITicketOrderRepository repository;
        private TicketCart cart;
        public OrderController(ITicketOrderRepository repoService, TicketCart cartService)
        {
            repository = repoService;
            cart = cartService;
        }
        //[Authorize]
        //public ViewResult List() =>
        //    View(repository.Orders.Where(o => !o.Shipped));
        //[HttpPost]
        //[Authorize]
        //public IActionResult MarkShipped(int orderID)
        //{
        //    TicketOrder order = repository.Orders.FirstOrDefault(o => o.OrderID == orderID);
        //    if (order != null)
        //    {
        //        order.Shipped = true; repository.SaveOrder(order);
        //    }
        //    return RedirectToAction(nameof(List));
        //}

        //public ViewResult Checkout() => View(new TicketOrder());
        //[HttpPost]
        //public IActionResult Checkout(TicketOrder order)
        //{
        //    if (cart.Lines.Count() == 0)
        //    {
        //        ModelState.AddModelError("", "Sorry, your cart is empty!");
        //    }
        //    if (ModelState.IsValid)
        //    {
        //        order.Lines = cart.Lines.ToArray();
        //        repository.SaveOrder(order);
        //        return RedirectToAction(nameof(Completed));
        //    }
        //    else
        //    {
        //        return View(order);
        //    }
        //}
        //public ViewResult Completed()
        //{
        //    cart.Clear();
        //    return View();
        //}
    }
}