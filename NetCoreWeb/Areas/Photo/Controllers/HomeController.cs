using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NetCoreWeb.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using NetCoreWeb.Areas.Photo.Models.ViewModels;
using NetCoreWeb.Areas.Photo.Models;

namespace NetCoreWeb.Areas.Photo.Controllers
{
    [Area("Photo")]
    public class HomeController : Controller
    {
        private IAlbumRepository repository;
        public HomeController(IAlbumRepository repo)
        {
            repository = repo;
        }
        public int PageSize = 4;
        public IActionResult List(string category, int page = 1)
        {
            return View(new AlbumListViewModel
            {
                Albums = repository.Albums
                .Where(p => category == null || p.Category == category)
                .OrderBy(p => p.AlbumID)
                .Skip((page - 1) * PageSize)
                .Take(PageSize),

                PagingInfo = new PagingInfo
                {
                    CurrentPage = page,
                    ItemsPerPage = PageSize,
                    TotalItems = category == null ? repository.Albums.Count() : repository.Albums.Where(e => e.Category == category).Count()
                },
                CurrentCategory = category
            });
        }
    }
}