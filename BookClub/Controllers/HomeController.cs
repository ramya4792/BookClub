using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using BookClub.Models;
using BookClub.Data;
using Microsoft.EntityFrameworkCore;

namespace BookClub.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ApplicationDbContext context;
        public HomeController(ILogger<HomeController> logger, ApplicationDbContext dbContext)
        {
            _logger = logger;
            context = dbContext;
        }
        

        public async Task<IActionResult> Index(string sortOrder, string currentFilter,  string searchTerm, int? pageNumber)
        {
            ViewData["CurrentSort"] = sortOrder;
            ViewData["TitleSortParm"] = String.IsNullOrEmpty(sortOrder) ? "title_desc" : "";
            ViewData["AuthorSortParm"] = String.IsNullOrEmpty(sortOrder) ? "author_desc" : "";
            ViewData["PriceSortParm"] = String.IsNullOrEmpty(sortOrder) ? "price_desc" : "";
            
            if(searchTerm != null)
            {
                pageNumber = 1;
            }
            else
            {
                searchTerm = currentFilter;
            }

            ViewData["CurrentFilter"] = searchTerm;
            var books = from b in context.Books
                        select b;
            if (!String.IsNullOrEmpty(searchTerm))
            {
                books = books.Where(b => b.Title.Contains(searchTerm)
                                    || b.Author.Contains(searchTerm));
            }
            switch (sortOrder)
            {
                case "title_desc":
                    books = books.OrderByDescending(b => b.Title);
                    break;
                case "author_desc":
                    books = books.OrderBy(b => b.Author);
                    break;
                case "price_desc":
                    books = books.OrderBy(b => b.Price);
                    break;

                default:
                    books = books.OrderBy(b => b.Title);
                    break;
            }
            return View(await PaginatedList<Book>.CreateAsync(books.AsNoTracking(), pageNumber ?? 1, 10));
        }

        public IActionResult Details(int bookId)
        {
            Book bookDetails = context.Books.Single(b => b.ID == bookId);
            ViewBag.BookTitle = bookDetails.Title;
            ViewBag.Author = bookDetails.Author;
            ViewBag.Desc = bookDetails.Description;
            ViewBag.Image = bookDetails.CoverPage;
            ViewBag.Cost = bookDetails.Price;
            ViewBag.Copy = bookDetails.Copy;

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        
    }
}
