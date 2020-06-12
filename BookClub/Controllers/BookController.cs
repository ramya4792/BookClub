using System;
using System.Collections.Generic;
using System.Linq;
using BookClub.Data;
using BookClub.Models;
using BookClub.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using System.IO;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BookClub.Controllers
{
    [Authorize]
    public class BookController : Controller
    {
        
        private readonly IWebHostEnvironment webHostEnvironment;
        private readonly ApplicationDbContext context;
        private readonly UserManager<ApplicationUser> userManager;
        public BookController(ApplicationDbContext dbContext, IWebHostEnvironment hostEnvironment, UserManager<ApplicationUser> user)
        {
            context = dbContext;
            webHostEnvironment = hostEnvironment;
            userManager = user;
        }
        public async Task<IActionResult> Index()
        {
      
            var user =  await userManager.FindByNameAsync(User.Identity.Name);

            IList<Book> books = context.Books.Include(b => b.BookCategory).Where(x=>x.UserId==user.Id).ToList();
            

            return View(books);
        }

        
        public IActionResult Add()
        {
            
            AddBookViewModel addBookViewModel = new AddBookViewModel(context.BookCategories.ToList());

            return View(addBookViewModel);
        }
        
        [HttpPost]
        public IActionResult Add(AddBookViewModel addBookViewModel)
        {       
            //extracting  the bookcategory text
            BookCategory bookCategory =
                context.BookCategories.Single(b => b.ID == addBookViewModel.BookCategoryID);
            var user = userManager.GetUserId(HttpContext.User);
            if (ModelState.IsValid)
            {
                Book newBook = new Book
                {
                    Title = addBookViewModel.Title,
                    Author = addBookViewModel.Author,
                    Description = addBookViewModel.Description,
                    BookCategory = bookCategory,
                    Copy = addBookViewModel.Copy,
                    PriceOption = addBookViewModel.PriceOption,
                    CoverPage = UploadedImge(addBookViewModel),
                    File = UploadedFile(addBookViewModel),
                    Price = addBookViewModel.Cost,
                    UserId = int.Parse(userManager.GetUserId(HttpContext.User))
                };
                context.Books.Add(newBook);
                context.SaveChanges();

                return Redirect("/Book");
            }
            return View(addBookViewModel);
        }
        private string UploadedFile(AddBookViewModel addBookViewModel)
        {
            string uniqueFileName = null;
            if (addBookViewModel.File != null)
            {
                // to get E:\CoderGirl\C#\ramya4792\BookClub\BookClub\wwwroot\Uploads\SoftCopies
                string uploadsFolder = Path.Combine(webHostEnvironment.WebRootPath, "Uploads", "SoftCopies");

                //creating unique filename using unique identifier and append the filename
                uniqueFileName = Guid.NewGuid().ToString() + "_" + addBookViewModel.File.FileName;

                //getting full file path
                string filePath = Path.Combine(uploadsFolder, uniqueFileName);

                using(var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    addBookViewModel.CoverPage.CopyTo(fileStream);
                }
            }
            return uniqueFileName;
        }
        private string UploadedImge(AddBookViewModel addBookViewModel)
        {
            string uniqueFileName = null;
            if (addBookViewModel.CoverPage != null)
            {
                // to get E:\CoderGirl\C#\ramya4792\BookClub\BookClub\wwwroot\Uploads\
                string uploadsFolder = Path.Combine(webHostEnvironment.WebRootPath, "Uploads");

                //creating unique filename using unique identifier and append the filename
                uniqueFileName = Guid.NewGuid().ToString() + "_" + addBookViewModel.CoverPage.FileName;

                //getting image path
                string filePath = Path.Combine(uploadsFolder, uniqueFileName);

                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    addBookViewModel.CoverPage.CopyTo(fileStream);
                }
            }
            return uniqueFileName;
        }
        public IActionResult Remove()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Remove(int bookId)
        {
            //get the book by bookId
            Book removeBook = context.Books.Single(b => b.ID == bookId);

            context.Books.Remove(removeBook);
            context.SaveChanges();
            if (removeBook.CoverPage != null)
            {
                //to remove image from folder
                string imagePath = Path.Combine(webHostEnvironment.WebRootPath, "Uploads");
                string removeImagePath = Path.Combine(imagePath, removeBook.CoverPage);
                //if (System.IO.File.Exists(removeImagePath))
                    System.IO.File.Delete(removeImagePath);
            }

            if (removeBook.File != null)
            {
                //to remove file from folder

                string filePath = Path.Combine(webHostEnvironment.WebRootPath, "Uploads", "SoftCopies");
                string removeFilePath = Path.Combine(filePath, removeBook.File);
                //if (System.IO.File.Exists(removeFilePath))
                    System.IO.File.Delete(removeFilePath);
            }
            return Redirect("/Book");
        }

        public IActionResult Edit(int bookId)
        {
            Book editBook = context.Books.Single(b => b.ID == bookId);
            EditBookViewModel editBookViewModel = new EditBookViewModel(context.BookCategories.ToList())
            {
                Title = editBook.Title,
                Author = editBook.Author,
                Description = editBook.Description,
                BookCategoryID = editBook.BookCategoryID,
                Copy = editBook.Copy,
                PriceOption = editBook.PriceOption,
                Cost = editBook.Price
            };
            return View(editBookViewModel);
        }

        [HttpPost]
        public IActionResult Edit(EditBookViewModel editBookViewModel)
        {
            string uniqueFileName = UploadedFile(editBookViewModel);
            Book editBook = context.Books.Single(b => b.ID == editBookViewModel.BookId);
            BookCategory bookCategory =
                context.BookCategories.Single(b => b.ID == editBookViewModel.BookCategoryID);

            if (ModelState.IsValid)
            {
                editBook.Title = editBookViewModel.Title;
                editBook.Author = editBookViewModel.Author;
                editBook.Description = editBookViewModel.Description;
                editBook.BookCategory = bookCategory;
                editBook.CoverPage = uniqueFileName;
                editBook.Copy = editBookViewModel.Copy;
                editBook.Price = editBookViewModel.Cost;

                context.SaveChanges();
                return Redirect("/Book");
            }
            return View(editBookViewModel);
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

        public IActionResult MessageDisplay()
        {
            return View();
        }
        
    }
}
