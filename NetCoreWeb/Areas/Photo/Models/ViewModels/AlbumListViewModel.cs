using NetCoreWeb.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NetCoreWeb.Areas.Photo.Models.ViewModels
{
    public class AlbumListViewModel
    {
        public IEnumerable<Album> Albums { get; set; }
        public PagingInfo PagingInfo { get; set; }
        public string CurrentCategory { get; set; }
    }
}
