using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using NetCoreWeb.Models.SuperHui;

namespace NetCoreWeb.Controllers.SuperHui
{
    public class MenuController : Controller
    {
        private IDishRepository repository;
        public MenuController(IDishRepository repo)
        {
            repository = repo;
        }
        public IActionResult List()
        {
            return View();
        }
    }
}