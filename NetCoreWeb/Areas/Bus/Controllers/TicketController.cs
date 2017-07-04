using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using NetCoreWeb.Areas.Bus.Models;
using NetCoreWeb.Models.ViewModels;
using NetCoreWeb.Areas.Bus.Models.ViewModels;

namespace NetCoreWeb.Areas.Bus.Controllers
{
    [Area("Bus")]
    public class TicketController : Controller
    {
        private ITicketRepository repository;
        public TicketController(ITicketRepository repo)
        {
            repository = repo;
        }
        public int PageSize = 4;
        public ViewResult List(string category, int page = 1) =>
            View(new TicketsListViewModel
            {
                Tickets = repository.Tickets
                .Where(t => category == null || t.Category == category)
                .OrderBy(t => t.TicketID)
                .Skip((page - 1) * PageSize)
                .Take(PageSize),

                PagingInfo = new PagingInfo
                {
                    CurrentPage = page,
                    ItemsPerPage = PageSize,
                    TotalItems = category == null ? repository.Tickets.Count() : repository.Tickets.Where(e => e.Category == category).Count()
                },
                CurrentCategory = category
            });        
    }
}