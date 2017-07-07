using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using NetCoreWeb.Areas.Bus.Models;

namespace NetCoreWeb.Migrations.BusTicketDb
{
    [DbContext(typeof(BusTicketDbContext))]
    [Migration("20170707024147_TicketOrder")]
    partial class TicketOrder
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "1.1.2")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("NetCoreWeb.Areas.Bus.Models.Ticket", b =>
                {
                    b.Property<int>("TicketID")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Category")
                        .IsRequired();

                    b.Property<string>("Description")
                        .IsRequired();

                    b.Property<decimal>("Price");

                    b.Property<string>("StartStation")
                        .IsRequired();

                    b.Property<string>("TerminalStation")
                        .IsRequired();

                    b.HasKey("TicketID");

                    b.ToTable("Tickets");
                });

            modelBuilder.Entity("NetCoreWeb.Areas.Bus.Models.TicketCartLine", b =>
                {
                    b.Property<int>("TicketCartLineID")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("Quantity");

                    b.Property<int?>("TicketID");

                    b.Property<int?>("TicketOrderID");

                    b.HasKey("TicketCartLineID");

                    b.HasIndex("TicketID");

                    b.HasIndex("TicketOrderID");

                    b.ToTable("TicketCartLine");
                });

            modelBuilder.Entity("NetCoreWeb.Areas.Bus.Models.TicketOrder", b =>
                {
                    b.Property<int>("TicketOrderID")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("OrderTime");

                    b.Property<bool>("Paid");

                    b.Property<string>("Phone")
                        .IsRequired();

                    b.Property<string>("Remarks");

                    b.HasKey("TicketOrderID");

                    b.ToTable("TicketOrders");
                });

            modelBuilder.Entity("NetCoreWeb.Areas.Bus.Models.TicketCartLine", b =>
                {
                    b.HasOne("NetCoreWeb.Areas.Bus.Models.Ticket", "Ticket")
                        .WithMany()
                        .HasForeignKey("TicketID");

                    b.HasOne("NetCoreWeb.Areas.Bus.Models.TicketOrder")
                        .WithMany("Lines")
                        .HasForeignKey("TicketOrderID");
                });
        }
    }
}
