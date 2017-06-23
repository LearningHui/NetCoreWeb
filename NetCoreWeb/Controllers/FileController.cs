using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.Net.Http.Headers;
using Microsoft.AspNetCore.Hosting;
using System.IO;
using Microsoft.AspNetCore.Cors;

namespace NetCoreWeb.Controllers
{
    [EnableCors("AllowSpecificOrigin")]
    public class FileController : Controller
    {
        private IHostingEnvironment hostingEnv;
        string[] pictureFormatArray = { "png", "jpg", "jpeg", "bmp", "gif", "ico", "PNG", "JPG", "JPEG", "BMP", "GIF", "ICO" };
        public FileController(IHostingEnvironment env)
        {
            this.hostingEnv = env;
        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult UploadPictures()
        {
            return View();
        }
        [HttpPost]
        public IActionResult UploadPictures(IList<IFormFile> files)
        {
            long size = 0;
            foreach (var file in files)
            {
                var fileName = ContentDispositionHeaderValue
                                .Parse(file.ContentDisposition)
                                .FileName
                                .Trim('"');
                var filePath = hostingEnv.WebRootPath + $@"\Files\Pictures\";
                if (!Directory.Exists(filePath))
                {
                    Directory.CreateDirectory(filePath);
                }
                string suffix = fileName.Split('.')[1];
                if (!pictureFormatArray.Contains(suffix))
                {
                    //return Json(Return_Helper_DG.Error_Msg_Ecode_Elevel_HttpCode("the picture format not support ! you must upload files that suffix like 'png','jpg','jpeg','bmp','gif','ico'."));
                    ViewBag.Message = "the picture format not support ! you must upload files that suffix like 'png','jpg','jpeg','bmp','gif','ico'.";
                }
                fileName = Guid.NewGuid() + "." + suffix;
                string fileFullName = filePath + fileName;                
                using (FileStream fs = System.IO.File.Create(fileFullName))
                {
                    file.CopyTo(fs);
                    fs.Flush();
                }
                size += file.Length;
            }
            ViewBag.Message = $"{files.Count} file(s) / { size}bytes uploaded successfully!";
            return View();
        }
    }
}