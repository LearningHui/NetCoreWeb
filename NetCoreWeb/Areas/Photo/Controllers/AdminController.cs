using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using NetCoreWeb.Areas.Photo.Models;
using NetCoreWeb.Models.SuperHui;
using NetCoreWeb.Areas.Photo.Models.ViewModels;
using Microsoft.AspNetCore.Http;
using NetCoreWeb.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Net.Http.Headers;
using System.IO;

namespace NetCoreWeb.Areas.Photo.Controllers
{
    [Authorize]
    [Area("Photo")]
    public class AdminController : Controller
    {
        private IAlbumRepository repository;
        private SuperHuiDbContext context;
        private IHostingEnvironment hostingEnv;
        public AdminController(IAlbumRepository repo, SuperHuiDbContext ctx, IHostingEnvironment env)
        {
            repository = repo;
            context = ctx;
            hostingEnv = env;
        }
        public int PageSize = 4;
        public IActionResult List(string category, int page = 1)
        {
            return View(new AlbumListViewModel
            {
                Albums = repository.Albums
                .Where(p => category == null || p.Category == category)
                .OrderBy(p => p.AlbumID).Reverse()
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
        //[HttpPost]
        //public IActionResult Edit(Album album, IList<IFormFile> files)
        //{
        //    if (files != null && files.Count() > 0)
        //    {
        //        //string pictureName = SavePicture(files);//保存上送的图片
        //        //if (!string.IsNullOrEmpty(pictureName))
        //        //    dish.ImageName = pictureName;
        //    }
        //    Picture pic = new Picture() { PictureName = "IMG_0348.jpg" };
        //    context.Pictures.Add(pic);
        //    context.SaveChanges();

        //    if (album.Lines == null)
        //        album.Lines = new List<AlbumPictureLine>();
        //    album.Lines.Add(new AlbumPictureLine()
        //    {
        //        Picture = pic
        //    });
        //    repository.SaveAlbum(album);
        //    //TempData["message"] = $"{dish.Name} has been saved";
        //    return RedirectToAction("List");
        //}

        public IActionResult CreateAlbum()
        {
            return View("Edit", new Album());
        }
        public IActionResult Edit(int albumId)
        {
            var album = repository.Albums.FirstOrDefault(a => a.AlbumID == albumId);
            return View(album);
        }
        [HttpPost]
        public IActionResult Edit(Album album)
        {
            //if (album.Lines == null)
            //    album.Lines = new List<AlbumPictureLine>();
            //album.Lines.Add(new AlbumPictureLine()
            //{
            //    Picture = pic
            //});
            album.CreateTime = DateTime.Now;
            repository.SaveAlbum(album);
            //TempData["message"] = $"{dish.Name} has been saved";
            return RedirectToAction("List");
        }
        public IActionResult Delete(int albumId)
        {
            //var album = repository.Albums.FirstOrDefault(a => a.AlbumID == albumId);
            repository.DeleteAlbum(albumId);
            return View();
        }



        //添加单张图片到相册
        public IActionResult AddPicture(int albumId)
        {
            ViewBag.AlbumId = albumId;
            return View(new Picture());
        }
        [HttpPost]
        public IActionResult CreatePicture(Picture picture, IList<IFormFile> files, int albumId)
        {
            if (ModelState.IsValid)
            {
                var album = repository.Albums.FirstOrDefault(a => a.AlbumID == albumId);
                if(album != null)
                {
                    if (album.Lines == null)
                        album.Lines = new List<AlbumPictureLine>();
                }
                else
                {
                    //todo:log error
                }
                if (files != null && files.Count() > 0)
                {
                    var filePath = hostingEnv.WebRootPath + $@"\Files\Pictures\Albums\{album.Name}\";
                    foreach (var file in files)
                    {
                        var pictureName = SavePicture(file, filePath);//保存上送的图片
                        if (!string.IsNullOrEmpty(pictureName))
                        {
                            picture.PictureName = pictureName;
                            picture.CreateTime = DateTime.Now;
                            context.Pictures.Add(picture);
                            album.Lines.Add(new AlbumPictureLine() { Picture = picture });
                            repository.SaveAlbum(album);
                            //TempData["message"] = $"{dish.Name} has been saved";  
                        }
                    }                     
                }                
                context.SaveChanges();
                return RedirectToAction("AddPicture", albumId);
            }
            else
            {
                // there is something wrong with the data values     
                return RedirectToAction("List");
            }
        }

        public ViewResult UploadPhoto(int albumId)=>
            View(repository.Albums.FirstOrDefault(a => a.AlbumID == albumId));

        //批量添加图片到相册
        [HttpPost]
        public IActionResult UploadPhoto(int albumId, IList<IFormFile> files)
        {
            if (ModelState.IsValid)
            {
                var album = repository.Albums.FirstOrDefault(a => a.AlbumID == albumId);
                if (album != null)
                {
                    if (album.Lines == null)
                        album.Lines = new List<AlbumPictureLine>();
                }
                else
                {
                    //todo:log error
                }
                if (files != null && files.Count() > 0)
                {
                    var filePath = hostingEnv.WebRootPath + $@"\Files\Pictures\Albums\{album.Name}\";
                    foreach (var file in files)
                    {
                        var pictureName = SavePicture(file, filePath);//保存上送的图片
                        if (!string.IsNullOrEmpty(pictureName))
                        {
                            var picture = new Picture();
                            picture.PictureName = pictureName;
                            picture.CreateTime = DateTime.Now;
                            context.Pictures.Add(picture);
                            album.Lines.Add(new AlbumPictureLine() { Picture = picture });
                            repository.SaveAlbum(album);
                        }
                    }
                }
                context.SaveChanges();
                TempData["message"] = $"成功上传 {files.Count()}张照片到{album.Name}";
                return View(album);
            }
            else
            {
                // there is something wrong with the data values     
                return RedirectToAction("List");
            }
        }



        public ViewResult DeletePhoto(int albumId,int albumPictureLineId)//软删除
        {
            bool isDelSucess = false;
            var album = repository.Albums.FirstOrDefault(a => a.AlbumID == albumId);
            if (album != null && album.Lines != null)
            {
                var matchedPhoto = album.Lines.FirstOrDefault(p => p.AlbumPictureLineID == albumPictureLineId);
                if (matchedPhoto != null)
                {
                    matchedPhoto.Delete = true;
                    isDelSucess = true;
                    context.SaveChanges();
                }
            }
            return View();
        }
        public ViewResult DeletePhotoCompletely(int albumId, int albumPictureLineId)
        {
            bool isDelSucess = false;
            var album = repository.Albums.FirstOrDefault(a => a.AlbumID == albumId);
            if (album != null && album.Lines != null)
            {
                var matchedPhoto = album.Lines.FirstOrDefault(p => p.AlbumPictureLineID == albumPictureLineId);
                if (matchedPhoto != null)
                {
                    album.Lines.Remove(matchedPhoto);
                    var filePath = hostingEnv.WebRootPath + $@"\Files\Pictures\Albums\{album.Name}\{matchedPhoto.Picture.PictureName}";
                    System.IO.File.Delete(filePath);
                    //Directory.Delete(filePath, true);
                    context.SaveChanges();
                }
            }
            return View();
        }
        /// <summary>
        /// 保存上传的图片
        /// </summary>
        /// <param name="files">保存的图片名称</param>
        /// <returns></returns>
        private string SavePicture(IFormFile file,string filePath)
        {
            long size = 0;
            try
            {
                var fileName = ContentDispositionHeaderValue
                                    .Parse(file.ContentDisposition)
                                    .FileName
                                    .Trim('"');
                
                if (!Directory.Exists(filePath))
                {
                    Directory.CreateDirectory(filePath);
                }
                string suffix = fileName.Substring(fileName.LastIndexOf(".") + 1, (fileName.Length - fileName.LastIndexOf(".") - 1));
                string[] pictureFormatArray = { "png", "jpg", "jpeg", "bmp", "gif", "ico", "PNG", "JPG", "JPEG", "BMP", "GIF", "ICO" };
                if (!pictureFormatArray.Contains(suffix))
                {

                }
                fileName = Guid.NewGuid() + "." + suffix;
                string fileFullName = filePath + fileName;
                using (FileStream fs = System.IO.File.Create(fileFullName))
                {
                    file.CopyTo(fs);
                    fs.Flush();
                }
                size += file.Length;
                return fileName;
            }

            catch (Exception)
            {
                return null;
            }
        }
    }
}