using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using NetCoreWeb.Areas.Bus.Models;

namespace NetCoreWeb.Migrations.BusTicketDb
{
    [DbContext(typeof(BusTicketDbContext))]
    [Migration("20170703124410_Initial")]
    partial class Initial
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
        }
    }
}
