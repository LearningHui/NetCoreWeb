using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using NetCoreWeb.Areas.Cooking.Models;
using NetCoreWeb.Areas.Cooking.Models.ViewModels;
using NetCoreWeb.Models.ViewModels;

namespace NetCoreWeb.Areas.Cooking.Controllers
{
    [Area("Cooking")]
    public class DishController : Controller
    {
        private IDishRepository repository;
        public DishController(IDishRepository repo)
        {
            repository = repo;
        }
        public int PageSize = 3;
        public ViewResult List(string category, int page = 1) =>
            View(new DishesListViewModel
            {
                Dishes = repository.Dishes
                .Where(p => category == null || p.Category == category)
                .OrderBy(p => p.DishID)
                .Skip((page - 1) * PageSize)
                .Take(PageSize),

                PagingInfo = new PagingInfo
                {
                    CurrentPage = page,
                    ItemsPerPage = PageSize,
                    TotalItems = category == null ? repository.Dishes.Count() : repository.Dishes.Where(e => e.Category == category).Count()
                },
                CurrentCategory = category
            });
    }
}