using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace NetCoreWeb.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult About()
        {
            ViewData["Message"] = "关于本网站的说明";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "辉哥的联系方式";

            return View();
        }

        public IActionResult Error()
        {
            return View();
        }
    }
}
