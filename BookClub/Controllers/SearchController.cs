using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookClub.Data;
using BookClub.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BookClub.Controllers
{
    public class SearchController : Controller
    {
        private readonly ApplicationDbContext context;
        
        public SearchController(ApplicationDbContext dbContext)
        {
            context = dbContext;
        }
        
        public IActionResult Index(string searchTerm)
        {
            
            var results = from books in context.Books
                          select books;
            if(searchTerm != null)
                results = results.Where(b => b.Title.Contains(searchTerm) || b.Author.Contains(searchTerm));
                
            return View(results.ToList());
        }

        
    }
}
