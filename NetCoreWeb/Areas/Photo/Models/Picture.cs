using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NetCoreWeb.Areas.Photo.Models
{
    public class Picture
    {
        public int PictureID { get; set; }
        public string PictureName { get; set; }
        public string Info { get; set; }
        public string Remark { get; set; }
        public bool Disabled { get; set; }
        public DateTime CreateTime { get; set; }
    }
    public class AlbumPictureLine
    {
        public int AlbumPictureLineID { get; set; }
        public Picture Picture { get; set; }
    }
}
