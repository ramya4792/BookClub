using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookClub.Data;
using BookClub.Models;
using BookClub.ViewModels;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BookClub.Controllers
{
    public class BookCategoryController : Controller
    {
        // GET: /<controller>/

        //adding categories of book
        private readonly ApplicationDbContext context;
        public BookCategoryController(ApplicationDbContext dbContext)
        {
            context = dbContext;
        }
        public IActionResult Index()
        {
            return View(context.BookCategories.ToList());
        }

        public IActionResult Add(AddBookCategoryViewModel addBookCategoryViewModel)
        {
            return View(addBookCategoryViewModel);
        }

        [HttpPost]
        public IActionResult Add(AddBookCategoryViewModel addBookCategoryViewModel, string subject)
        {
            if (ModelState.IsValid)
            {
                BookCategory newCategory = new BookCategory
                {
                    Subject = subject
                };
                context.BookCategories.Add(newCategory);
                context.SaveChanges();
                return Redirect("/BookCategory");
            }
            return View();
        }
    }
}
