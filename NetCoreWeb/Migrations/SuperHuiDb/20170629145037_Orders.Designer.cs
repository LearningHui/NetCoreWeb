using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using NetCoreWeb.Models.SuperHui;

namespace NetCoreWeb.Migrations.SuperHuiDb
{
    [DbContext(typeof(SuperHuiDbContext))]
    [Migration("20170629145037_Orders")]
    partial class Orders
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

            modelBuilder.Entity("NetCoreWeb.Models.SuperHui.MenuLine", b =>
                {
                    b.Property<int>("MenuLineID")
                        .ValueGeneratedOnAdd();

                    b.Property<int?>("DishID");

                    b.Property<int?>("OrderID");

                    b.Property<int>("Quantity");

                    b.HasKey("MenuLineID");

                    b.HasIndex("DishID");

                    b.HasIndex("OrderID");

                    b.ToTable("MenuLine");
                });

            modelBuilder.Entity("NetCoreWeb.Models.SuperHui.Order", b =>
                {
                    b.Property<int>("OrderID")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Mobile")
                        .IsRequired();

                    b.Property<string>("Name")
                        .IsRequired();

                    b.HasKey("OrderID");

                    b.ToTable("Orders");
                });

            modelBuilder.Entity("NetCoreWeb.Models.SuperHui.MenuLine", b =>
                {
                    b.HasOne("NetCoreWeb.Models.SuperHui.Dish", "Dish")
                        .WithMany()
                        .HasForeignKey("DishID");

                    b.HasOne("NetCoreWeb.Models.SuperHui.Order")
                        .WithMany("Lines")
                        .HasForeignKey("OrderID");
                });
        }
    }
}
