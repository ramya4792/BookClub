using BookClub.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BookClub.ViewModels
{
    public class AddBookViewModel
    {
        [Required]
        [Display(Name = "Title of the Book")]
        public string Title { get; set; }

        [Required]
        public string Author { get; set; }

        [Required]
        [Display(Name ="Category")]
        public int BookCategoryID { get; set; }
        public List<SelectListItem> BookCategories { get; set; }


        //default constructor
        public AddBookViewModel() { }

        //constructor with categories as a parameter
        public AddBookViewModel(IEnumerable<BookCategory> categories)
        {
            BookCategories = new List<SelectListItem>();
            foreach(var category in categories)
            {
                BookCategories.Add(new SelectListItem
                {
                    Value = category.ID.ToString(),
                    Text = category.Subject
                });
                
            }
        }

        
    }
}
