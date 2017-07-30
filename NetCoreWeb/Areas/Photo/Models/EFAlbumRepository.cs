using NetCoreWeb.Models.SuperHui;
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace NetCoreWeb.Areas.Photo.Models
{
    public class EFAlbumRepository : IAlbumRepository
    {
        private SuperHuiDbContext context;
        public EFAlbumRepository(SuperHuiDbContext ctx) { context = ctx; }
        public IEnumerable<Album> Albums => context.Albums.Include(o => o.Lines).ThenInclude(l => l.Picture);
        public void SaveAlbum(Album album)
        {
            //if(album.Lines != null)
            //    context.AttachRange(album.Lines.Select(a => a.Picture));
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
                    dbEntry.Lines = album.Lines;
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
