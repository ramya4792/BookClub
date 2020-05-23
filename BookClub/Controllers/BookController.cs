using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookClub.Data;
using BookClub.Models;
using BookClub.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BookClub.Controllers
{
    public class BookController : Controller
    {
        // GET: /<controller>/

        private readonly ApplicationDbContext context;
        public BookController(ApplicationDbContext dbContext)
        {
            context = dbContext;
        }
        public IActionResult Index()
        {
            IList<Book> books = context.Books.Include(b => b.BookCategory).ToList();
            return View(books);
        }
        public IActionResult Add()
        {
            AddBookViewModel addBookViewModel = new AddBookViewModel(context.BookCategories.ToList());

            return View(addBookViewModel);
        }

        [HttpPost]
        public IActionResult Add(AddBookViewModel addBookViewModel )
        {
            BookCategory bookCategory =
                context.BookCategories.Single(b => b.ID == addBookViewModel.BookCategoryID);
            if (ModelState.IsValid)
            {
                Book newBook = new Book
                {
                    Title = addBookViewModel.Title,
                    Author = addBookViewModel.Author,
                    BookCategory = bookCategory
                    
                };
                context.Books.Add(newBook);
                context.SaveChanges();

                return Redirect("/Book");
            }
            return View(addBookViewModel);
        }
        public IActionResult Remove()
        {
            ViewBag.Title = "Remove Books";
            ViewBag.books = context.Books.ToList();
            return View();
        }

        [HttpPost]
        public IActionResult Remove(int[] bookIds)
        {
            foreach(int bookId in bookIds)
            {
                Book removeCheese = context.Books.Single(b => b.ID == bookId);
                context.Books.Remove(removeCheese);
            }
            context.SaveChanges();
            return Redirect("/Book");
        }

        public IActionResult Edit(int bookId)
        {
            Book editBook = context.Books.Single(b => b.ID == bookId);
            EditBookViewModel editBookViewModel = new EditBookViewModel(context.BookCategories.ToList())
            {
                Title = editBook.Title,
                Author = editBook.Author,
                BookCategoryID = editBook.BookCategoryID
            };
            return View(editBookViewModel);
        }

        [HttpPost]
        public IActionResult Edit(EditBookViewModel editBookViewModel)
        {
            Book editBook = context.Books.Single(b => b.ID == editBookViewModel.BookId);
            BookCategory bookCategory =
                context.BookCategories.Single(b => b.ID == editBookViewModel.BookCategoryID);

            if (ModelState.IsValid)
            {
                editBook.Title = editBookViewModel.Title;
                editBook.Author = editBookViewModel.Author;
                editBook.BookCategory = bookCategory;

                context.SaveChanges();
                return Redirect("/Book");
            }
            return View(editBookViewModel);
        }
    }
}
