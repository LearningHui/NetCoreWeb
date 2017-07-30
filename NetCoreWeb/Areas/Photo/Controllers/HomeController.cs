using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NetCoreWeb.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using NetCoreWeb.Areas.Photo.Models.ViewModels;
using NetCoreWeb.Areas.Photo.Models;
using Microsoft.AspNetCore.Http;
using NetCoreWeb.Models.SuperHui;
using Microsoft.EntityFrameworkCore;

namespace NetCoreWeb.Areas.Photo.Controllers
{
    [Area("Photo")]
    public class HomeController : Controller
    {
        private IAlbumRepository repository;
        private SuperHuiDbContext context;
        public HomeController(IAlbumRepository repo, SuperHuiDbContext ctx)
        {
            repository = repo;
            context = ctx;
        }
        public int PageSize = 4;
        public IActionResult List(string category, int page = 1)
        {
            var albums = repository.Albums;
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

        public IActionResult Album(int albumID)
        {
            var album = repository.Albums.FirstOrDefault(a => a.AlbumID == albumID);
            if (album != null)
            {

                return View(album);
            }
            else
            {
                return View("List");//未匹配到，跳转列表页面。此处后期优化
            }
        }
    }
}