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
        public ViewResult List() =>
            //View(repository.Orders.Where(o => !o.Paid));
            View(repository.Orders);
        //[HttpPost]
        //[Authorize]
        public IActionResult MarkPaid(int orderID)
        {
            TicketOrder order = repository.Orders.FirstOrDefault(o => o.TicketOrderID == orderID);
            if (order != null)
            {
                order.Paid = true;
                repository.SaveOrder(order);                
                Utility.Sms.SendSms.SendNotice("3063716", new string[] { $"{order.Phone}"}, new string[] { "7点15"});
            }
            return RedirectToAction(nameof(List));
        }

        public ViewResult Checkout()
        {
            return View(new TicketOrder());
        }
        [HttpPost]
        public IActionResult Checkout(TicketOrder order)
        {
            if (cart.Lines.Count() == 0)
            {
                ModelState.AddModelError("", "你还没有选购车票,请先选择车票后下单！");
            }
            if (ModelState.IsValid)
            {
                order.Lines = cart.Lines.ToArray();
                order.OrderTime = DateTime.Now;
                repository.SaveOrder(order);
                return RedirectToAction(nameof(Completed));
            }
            else
            {
                return View(order);
            }
        }
        public ViewResult Completed()
        {
            cart.Clear();
            return View();
        }
    }
}