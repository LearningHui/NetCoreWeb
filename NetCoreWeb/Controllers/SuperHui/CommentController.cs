using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using NetCoreWeb.Models.SuperHui;
using NetCoreWeb.Models.SuperHui.ViewModels;

namespace NetCoreWeb.Controllers.SuperHui
{
    public class CommentController : Controller
    {
        private ICommentRepository repository;
        public CommentController(ICommentRepository repo)
        {
            repository = repo;
        }
        //per page size
        public int PageSize=4;
        //comments list
        public ViewResult List(int page = 1)
        {
            return View(new CommentsListViewModel
            {
                Comments=repository.Comments
                .OrderBy(c=>c.CommentID)
                .Skip((page - 1) * PageSize)
                .Take(PageSize),
                PagingInfo = new PagingInfo
                {
                    CurrentPage = page,
                    ItemsPerPage = PageSize,
                    TotalItems = repository.Comments.Count()
                }
            });
        }
        public IActionResult Create()
        {
            return View(new Comment());
        }
        [HttpPost]
        public IActionResult Create(Comment comment)
        {
            if (ModelState.IsValid)
            {
                comment.Time = DateTime.Now;
                repository.SaveProduct(comment);
                return RedirectToAction("List");
            }
            else
            {
                // there is something wrong with the data values     
                return View("List");
            }            
        }
    }
}