using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace NetCoreWeb.Areas.Photo.Controllers
{
    [Area("Photo")]
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}