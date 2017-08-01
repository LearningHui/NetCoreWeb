using Microsoft.EntityFrameworkCore;
using NetCoreWeb.Areas.Cooking.Models;
using NetCoreWeb.Areas.Photo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NetCoreWeb.Models.SuperHui
{
    public class SuperHuiDbContext : DbContext
    {
        public SuperHuiDbContext(DbContextOptions<SuperHuiDbContext> options) : base(options) { }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Dish> Dishes { get; set; }
        public DbSet<Picture> Pictures { get; set; }
        public DbSet<Album> Albums { get; set; } 
        public DbSet<AlbumPictureLine> AlbumPictureLine { get; set; }
    }
}
