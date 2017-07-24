using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NetCoreWeb.Areas.Photo.Models
{
    public interface IAlbumRepository
    {
        IEnumerable<Album> Albums { get; }
        void SaveAlbum(Album album);
        Album DeleteAlbum(int albumID);
    }
}
