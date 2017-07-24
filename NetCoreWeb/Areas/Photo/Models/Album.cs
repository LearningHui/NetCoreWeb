using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NetCoreWeb.Areas.Photo.Models
{
    public class Album
    {
        public int AlbumID { get; set; }
        public string Name { get; set; }
        public string Category { get; set; }//影集类别
        public string Description { get; set; }//描述
        public DateTime CreateTime { get; set; }//创建时间       
        public bool Disabled { get; set; }//是否可用（删除）        
    }
}
