using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using NetCoreWeb.Models.SuperHui;
using NetCoreWeb.Models.SuperHui.ViewModels;

namespace NetCoreWeb.Controllers.SuperHui
{
    public class MenuController : Controller
    {
        private IDishRepository repository;
        private Menu menu;
        public MenuController(IDishRepository repo, Menu menuService)
        {
            repository = repo;
            menu = menuService;
        }
        public ViewResult Index(string returnUrl)
        {
            return View(new MenuIndexViewModel
            {
                Menu = menu,
                ReturnUrl = returnUrl
            });
        }
        public RedirectToActionResult AddToMenu(int dishId, string returnUrl)
        {
            Dish dish = repository.Dishes.FirstOrDefault(p => p.DishID == dishId);
            if (dish != null)
            {
                menu.AddItem(dish, 1);
            }
            return RedirectToAction("Index", new { returnUrl });
        }
    }
}