using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace NetCoreWeb.Areas.Photo.Models
{
    public class Picture
    {
        public int PictureID { get; set; }
        public string PictureName { get; set; }
        [NotMapped]
        public string ThumbnailName
        {
            get
            {
                return PictureName.Insert(PictureName.LastIndexOf("."), "-thumbnail");                
            }
        }
        public string Info { get; set; }
        public string Remark { get; set; }
        public bool Disabled { get; set; }
        public DateTime CreateTime { get; set; }
        /// <summary>
        /// 获取不同倍率图的路径
        /// </summary>
        /// <param name="width">图片宽度</param>
        /// <returns></returns>
        public string GetRatePicName(int width)
        {
            return PictureName.Insert(PictureName.LastIndexOf("."), "-w" + width);
        }
    }
    public class AlbumPictureLine
    {
        public int AlbumPictureLineID { get; set; }
        public Picture Picture { get; set; }
        public bool Delete { get; set; }
    }
}
