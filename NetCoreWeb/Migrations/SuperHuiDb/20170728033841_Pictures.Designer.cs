using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using NetCoreWeb.Models.SuperHui;

namespace NetCoreWeb.Migrations.SuperHuiDb
{
    [DbContext(typeof(SuperHuiDbContext))]
    [Migration("20170728033841_Pictures")]
    partial class Pictures
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "1.1.2")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("NetCoreWeb.Areas.Cooking.Models.Dish", b =>
                {
                    b.Property<int>("DishID")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Category");

                    b.Property<string>("Description");

                    b.Property<bool>("Disabled");

                    b.Property<string>("ImageName");

                    b.Property<string>("Name");

                    b.Property<string>("Remark");

                    b.HasKey("DishID");

                    b.ToTable("Dishes");
                });

            modelBuilder.Entity("NetCoreWeb.Areas.Photo.Models.Album", b =>
                {
                    b.Property<int>("AlbumID")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Category");

                    b.Property<DateTime>("CreateTime");

                    b.Property<string>("Description");

                    b.Property<bool>("Disabled");

                    b.Property<string>("Name");

                    b.HasKey("AlbumID");

                    b.ToTable("Albums");
                });

            modelBuilder.Entity("NetCoreWeb.Areas.Photo.Models.AlbumPictureLine", b =>
                {
                    b.Property<int>("AlbumPictureLineID")
                        .ValueGeneratedOnAdd();

                    b.Property<int?>("AlbumID");

                    b.Property<int?>("PictureID");

                    b.HasKey("AlbumPictureLineID");

                    b.HasIndex("AlbumID");

                    b.HasIndex("PictureID");

                    b.ToTable("AlbumPictureLine");
                });

            modelBuilder.Entity("NetCoreWeb.Areas.Photo.Models.Picture", b =>
                {
                    b.Property<int>("PictureID")
                        .ValueGeneratedOnAdd();

                    b.Property<bool>("Disabled");

                    b.Property<string>("Info");

                    b.Property<string>("PictureName");

                    b.Property<string>("Remark");

                    b.HasKey("PictureID");

                    b.ToTable("Pictures");
                });

            modelBuilder.Entity("NetCoreWeb.Models.SuperHui.Comment", b =>
                {
                    b.Property<int>("CommentID")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Content")
                        .IsRequired();

                    b.Property<DateTime>("Time");

                    b.HasKey("CommentID");

                    b.ToTable("Comments");
                });

            modelBuilder.Entity("NetCoreWeb.Areas.Photo.Models.AlbumPictureLine", b =>
                {
                    b.HasOne("NetCoreWeb.Areas.Photo.Models.Album")
                        .WithMany("Lines")
                        .HasForeignKey("AlbumID");

                    b.HasOne("NetCoreWeb.Areas.Photo.Models.Picture", "Picture")
                        .WithMany()
                        .HasForeignKey("PictureID");
                });
        }
    }
}
