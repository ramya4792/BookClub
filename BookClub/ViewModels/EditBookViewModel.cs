using BookClub.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookClub.ViewModels
{
    public class EditBookViewModel:AddBookViewModel
    {
        public int BookId { get; set; }

        //default constructor
        public EditBookViewModel() { }

        //constructor with categories as parameter
        public EditBookViewModel(IEnumerable<BookCategory> categories)
        {
            BookCategories = new List<SelectListItem>();
            foreach(BookCategory category in categories)
            {
                BookCategories.Add(new SelectListItem
                {
                    Value = category.ID.ToString(),
                    Text =category.Subject
                });
            }
        }
    }
}
