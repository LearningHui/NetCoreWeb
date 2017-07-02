using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace NetCoreWeb.Controllers.Bus
{
    public class BusHomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult BookTicket()
        {
            return View();
        }
        public IActionResult BookInfor()
        {
            return View();
        }
    }
}