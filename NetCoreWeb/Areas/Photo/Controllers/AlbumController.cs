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
using Microsoft.Net.Http.Headers;
using System.IO;
using ImageMagick;
using Microsoft.AspNetCore.Hosting;

namespace NetCoreWeb.Areas.Photo.Controllers
{
    [Area("Photo")]
    public class AlbumController : Controller
    {
        private IAlbumRepository repository;
        private SuperHuiDbContext context;
        private IHostingEnvironment hostingEnv;
        public AlbumController(IAlbumRepository repo, SuperHuiDbContext ctx, IHostingEnvironment env)
        {
            repository = repo;
            context = ctx;
            hostingEnv =env;
        }
        public int PageSize = 4;
        public IActionResult List(string category, int page = 1)
        {
            return View(new AlbumListViewModel
            {
                Albums = repository.Albums
                .Where(p => category == null || p.Category == category)
                .OrderBy(p => p.CreateTime).Reverse()
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
        public IActionResult Gallery(string albumName)
        {
            var album = repository.Albums.FirstOrDefault(a => a.Name == albumName);
            if (album != null)
            {
                return View(nameof(Album),album);
            }
            else
            {
                return View("List");//未匹配到，跳转列表页面。此处后期优化
            }
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

        public ViewResult Detail(string photoSrc)
        {
            photoSrc = photoSrc.Replace("/", "\\");
            string photoPath = hostingEnv.WebRootPath + $@"\Files\Pictures{photoSrc}";
            using (MagickImage image = new MagickImage(photoPath))
            {
                ExifProfile profile = image.GetExifProfile();
                //相机型号
                ViewData["Model"] = profile.Values.FirstOrDefault(c => c.Tag == ExifTag.Model);
                //曝光时间
                ViewData["ExposureTime"] = profile.Values.FirstOrDefault(c => c.Tag == ExifTag.ExposureTime);
                //光圈
                ViewData["FNumber"] = profile.Values.FirstOrDefault(c => c.Tag == ExifTag.FNumber);
                //焦距
                ViewData["FocalLength"] = profile.Values.FirstOrDefault(c => c.Tag == ExifTag.FocalLength);

                //foreach (ExifValue value in profile.Values)
                //{
                //    System.Diagnostics.Debug.WriteLine(string.Format("{0}({1}): {2}", value.Tag, value.DataType, value.ToString()));                
                //}
            }
            return View((object)photoSrc);
        }        
        
    }

}