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
using Microsoft.AspNetCore.Authorization;

namespace NetCoreWeb.Controllers
{
    [Authorize]
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
        public IActionResult UploadFiles()
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
                string suffix = fileName.Substring(fileName.LastIndexOf(".") + 1, (fileName.Length - fileName.LastIndexOf(".") - 1));
                if (!pictureFormatArray.Contains(suffix))
                {
                    //return Json(Return_Helper_DG.Error_Msg_Ecode_Elevel_HttpCode("the picture format not support ! you must upload files that suffix like 'png','jpg','jpeg','bmp','gif','ico'."));
                    TempData["message"] = "the picture format not support ! you must upload files that suffix like 'png','jpg','jpeg','bmp','gif','ico'.";
                    return View();
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
            TempData["message"] = $"{files.Count} picture(s) / { FileCapability(size)} uploaded successfully!";
            return View();
        }
        [HttpPost]
        public IActionResult UploadFiles(IList<IFormFile> files)
        {
            long size = 0;
            foreach (var file in files)
            {
                var fileName = ContentDispositionHeaderValue
                                .Parse(file.ContentDisposition)
                                .FileName
                                .Trim('"');
                var filePath = hostingEnv.WebRootPath + $@"\Files\";
                if (!Directory.Exists(filePath))
                {
                    Directory.CreateDirectory(filePath);
                }
                //ÎÄ¼þÃû³Æ
                string _fileName = fileName.Substring(fileName.LastIndexOf("\\") + 1, (fileName.LastIndexOf(".") - fileName.LastIndexOf("\\") - 1));
                string suffix = fileName.Substring(fileName.LastIndexOf(".") + 1, (fileName.Length - fileName.LastIndexOf(".") - 1));                
                fileName = _fileName + "." + suffix;
                string fileFullName = filePath + fileName;
                using (FileStream fs = System.IO.File.Create(fileFullName))
                {
                    file.CopyTo(fs);
                    fs.Flush();
                }
                size += file.Length;
            }
            //TempData["message"] = $"{files.Count} file(s) / { size}bytes uploaded successfully!";
            TempData["message"] = $"{files.Count} file(s) / { FileCapability(size)} uploaded successfully!";

            return View();
        }
        //get file capability
        private string FileCapability(long bytes)
        {
            long KB = bytes / 1024;
            if (KB < 1024)
                return KB.ToString() + "KB";
            else if (KB >= 1024 && KB < 1024 * 1024)
                return (KB / 1024).ToString() + "MB";
            else
                return (KB / (1024*1024)).ToString() + "GB";
        }
    }
}