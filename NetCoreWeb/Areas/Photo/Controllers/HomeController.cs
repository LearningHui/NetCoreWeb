using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NetCoreWeb.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using NetCoreWeb.Areas.Photo.Models.ViewModels;

namespace NetCoreWeb.Areas.Photo.Controllers
{
    [Area("Photo")]
    public class HomeController : Controller
    {
        public int PageSize = 4;
        public IActionResult List(string category, int page = 1)
        {
            return View(new AlbumListViewModel
            {
                PagingInfo = new PagingInfo
                {
                    CurrentPage = page,
                    ItemsPerPage = PageSize,
                    TotalItems = 10
                },
                CurrentCategory = category
            });
        }
    }
}