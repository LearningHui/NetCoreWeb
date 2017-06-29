using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using NetCoreWeb.Models.SuperHui;

namespace NetCoreWeb.Migrations.SuperHuiDb
{
    [DbContext(typeof(SuperHuiDbContext))]
    [Migration("20170628155033_Dishes")]
    partial class Dishes
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "1.1.2")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

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

            modelBuilder.Entity("NetCoreWeb.Models.SuperHui.Dish", b =>
                {
                    b.Property<int>("DishID")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Category");

                    b.Property<string>("Description");

                    b.Property<bool>("Disabled");

                    b.Property<string>("ImageSrc");

                    b.Property<string>("Name");

                    b.Property<decimal>("Price");

                    b.Property<string>("Remark");

                    b.HasKey("DishID");

                    b.ToTable("Dishes");
                });
        }
    }
}
