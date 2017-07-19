using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using NetCoreWeb.Areas.Cooking.Models;
using Microsoft.AspNetCore.Http;
using System.IO;
using Microsoft.Net.Http.Headers;
using Microsoft.AspNetCore.Hosting;

namespace NetCoreWeb.Areas.Cooking.Controllers
{
    [Area("Cooking")]
    public class AdminController : Controller
    {
        private IDishRepository repository;
        private IHostingEnvironment hostingEnv;
        public AdminController(IDishRepository repo, IHostingEnvironment env)
        {
            repository = repo;
            hostingEnv = env;
        }
        public ViewResult Index()
        {
            return View(repository.Dishes);
        }
        public ViewResult Edit(int dishId) =>
            View(repository.Dishes.FirstOrDefault(p => p.DishID == dishId));
        [HttpPost]
        public IActionResult Edit(Dish dish, IList<IFormFile> files)
        {            

            if (ModelState.IsValid)
            {
                if (files.Count() > 0)
                {
                    string pictureName = SavePicture(files);//保存上送的图片
                    if (!string.IsNullOrEmpty(pictureName))
                        dish.ImageName = pictureName;
                }
                repository.SaveDish(dish);
                TempData["message"] = $"{dish.Name} has been saved";
                return RedirectToAction("Index");
            }
            else
            {
                // there is something wrong with the data values     
                return View(dish);
            }
        }
        public ViewResult Create() => View("Edit", new Dish());
        [HttpPost]
        public IActionResult Delete(int dishId)
        {
            Dish deletedDish = repository.DeleteDish(dishId);
            if (deletedDish != null)
            {
                TempData["message"] = $"{deletedDish.Name} was deleted";
            }
            return RedirectToAction("Index");
        }
        /// <summary>
        /// 保存上传的图片
        /// </summary>
        /// <param name="files">保存的图片名称</param>
        /// <returns></returns>
        private string SavePicture(IList<IFormFile> files)
        {
            long size = 0;
            try
            {
                var file = files.FirstOrDefault();
                if (file == null)
                    return null;
                var fileName = ContentDispositionHeaderValue
                                    .Parse(file.ContentDisposition)
                                    .FileName
                                    .Trim('"');
                var filePath = hostingEnv.WebRootPath + $@"\Files\Pictures\Dishes\";
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