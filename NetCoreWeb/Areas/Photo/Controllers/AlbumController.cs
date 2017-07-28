using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using NetCoreWeb.Areas.Photo.Models;
using NetCoreWeb.Models.SuperHui;
using NetCoreWeb.Areas.Photo.Models.ViewModels;
using NetCoreWeb.Models.ViewModels;
using Microsoft.AspNetCore.Http;

namespace NetCoreWeb.Areas.Photo.Controllers
{
    [Area("Photo")]
    public class AlbumController : Controller
    {
        private IAlbumRepository repository;
        private SuperHuiDbContext context;
        public AlbumController(IAlbumRepository repo, SuperHuiDbContext ctx)
        {
            repository = repo;
            context = ctx;
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
        [HttpPost]
        public IActionResult Edit(Album album, IList<IFormFile> files)
        {
            if (files.Count() > 0)
            {
                //string pictureName = SavePicture(files);//保存上送的图片
                //if (!string.IsNullOrEmpty(pictureName))
                //    dish.ImageName = pictureName;
            }
            Picture pic = new Picture() { PictureName = "IMG_0348.jpg" };
            context.Pictures.Add(pic);
            context.SaveChanges();

            if (album.Lines == null)
                album.Lines = new List<AlbumPictureLine>();
            album.Lines.Add(new AlbumPictureLine()
            {
                Picture = pic
            });
            repository.SaveAlbum(album);
            //TempData["message"] = $"{dish.Name} has been saved";
            return RedirectToAction("List");
        }
    }
}