using NetCoreWeb.Models.SuperHui;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NetCoreWeb.Areas.Photo.Models
{
    public class EFAlbumRepository : IAlbumRepository
    {
        private SuperHuiDbContext context;
        public EFAlbumRepository(SuperHuiDbContext ctx) { context = ctx; }
        public IEnumerable<Album> Albums => context.Albums;
        public void SaveAlbum(Album album)
        {
            if (album.AlbumID == 0)
            {
                context.Albums.Add(album);
            }
            else
            {
                Album dbEntry = context.Albums.FirstOrDefault(a => a.AlbumID == album.AlbumID);
                if (dbEntry != null)
                {
                    dbEntry.Name = album.Name;
                    dbEntry.Description = album.Description;                    
                    dbEntry.Category = album.Category;
                    dbEntry.CreateTime = album.CreateTime;
                    dbEntry.Disabled = album.Disabled;
                }
            }
            context.SaveChanges();
        }
        public Album DeleteAlbum(int albumID)
        {
            Album dbEntry = context.Albums.FirstOrDefault(a => a.AlbumID == albumID);
            if (dbEntry != null)
            {
                context.Albums.Remove(dbEntry);
                context.SaveChanges();
            }
            return dbEntry;
        }       
    }
}
