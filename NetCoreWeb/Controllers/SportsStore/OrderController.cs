using Microsoft.AspNetCore.Mvc;
using NetCoreWeb.Models.SportsStore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NetCoreWeb.Controllers.SportsStore
{
    public class OrderController : Controller 
    {
        public ViewResult Checkout() => View(new Order());
    }
}
