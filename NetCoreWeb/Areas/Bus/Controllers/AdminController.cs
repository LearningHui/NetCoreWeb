using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using NetCoreWeb.Areas.Bus.Models;

namespace NetCoreWeb.Areas.Bus.Controllers
{
    [Area("Bus")]
    public class AdminController : Controller
    {
        private ITicketRepository repository;
        public AdminController(ITicketRepository repo)
        {
            repository = repo;
        }
        public ViewResult Index() => View(repository.Tickets);
        public ViewResult Edit(int ticketId)
        {
            var ticket = repository.Tickets.FirstOrDefault(p => p.TicketID == ticketId);
            return View(ticket);
        } 
            
        [HttpPost]
        public IActionResult Edit(Ticket ticket)
        {
            if (ModelState.IsValid)
            {
                repository.SaveProduct(ticket);
                TempData["message"] = $"{ticket.StartStation}-{ticket.TerminalStation} 票务信息已修改！";
                return RedirectToAction("Index");
            }
            else
            {
                // there is something wrong with the data values     
                return View(ticket);
            }
        }
        public ViewResult Create() => View("Edit", new Ticket());
        [HttpPost]
        public IActionResult Delete(int ticketId)
        {
            Ticket deletedProduct = repository.DeleteTicket(ticketId);
            if (deletedProduct != null)
            {
                TempData["message"] = $"{deletedProduct.StartStation} - {deletedProduct.TerminalStation} 删除完成！";
            }
            return RedirectToAction("Index");
        }
    }
}