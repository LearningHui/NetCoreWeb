using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace NetCoreWeb.Areas.Test.Controllers
{
    [Area("Test")]
    public class CssController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}