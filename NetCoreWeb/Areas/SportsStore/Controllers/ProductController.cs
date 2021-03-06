using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using NetCoreWeb.Models.ViewModels;
using NetCoreWeb.Areas.SportsStore.Models;
using NetCoreWeb.Areas.SportsStore.Models.ViewModels;

namespace NetCoreWeb.Areas.SportsStore.Controllers
{
    [Area("SportsStore")]
    public class ProductController : Controller
    {
        private IProductRepository repository;
        public ProductController(IProductRepository repo)
        {
            repository = repo;
        }
        public int PageSize = 4;
        public ViewResult List(string category, int page = 1) => 
            View(new ProductsListViewModel
            {
                Products = repository.Products
                .Where(p => category == null || p.Category == category)
                .OrderBy(p => p.ProductID)
                .Skip((page - 1) * PageSize)
                .Take(PageSize),

                PagingInfo = new PagingInfo {
                    CurrentPage = page,
                    ItemsPerPage = PageSize,
                    TotalItems = category == null ? repository.Products.Count() : repository.Products.Where(e => e.Category == category).Count()
                },
                CurrentCategory = category
            });
    }
}