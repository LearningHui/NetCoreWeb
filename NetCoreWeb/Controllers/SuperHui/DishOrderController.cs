using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NetCoreWeb.Models.SuperHui;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NetCoreWeb.Controllers.SuperHui
{
    public class DishOrderController : Controller
    {
        private IDishOrderRepository repository;
        private Menu menu;
        public DishOrderController(IDishOrderRepository repoService, Menu menuService)
        {
            repository = repoService;
            menu = menuService;
        }
        [Authorize]
        public ViewResult List() =>
            View(repository.Orders);

        public ViewResult Order() => View(new Order());
        [HttpPost]
        public IActionResult Order(Order order)
        {
            if (menu.Lines.Count() == 0)
            {
                ModelState.AddModelError("", "Sorry, your menu is empty!");
            }
            if (ModelState.IsValid)
            {
                order.Lines = menu.Lines.ToArray();
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
            menu.Clear();
            return View();
        }
    }
}
