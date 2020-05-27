using BookClub.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BookClub.ViewModels
{

    public class AddBookViewModel
    {
        [Required(ErrorMessage ="Title required")]
        [Display(Name = "Title of the Book")]
        
        public string Title { get; set; }

        [Required(ErrorMessage ="Author required")]
        public string Author { get; set; }

        public string Description { get; set; }

        [Required]
        [Display(Name ="Category")]
        public int BookCategoryID { get; set; }
        public List<SelectListItem> BookCategories { get; set; }


        [Display(Name ="Cover Page of Book")]
        public IFormFile CoverPage { get; set; }

        
        [Required]
        public BookCopy Copy { get; set; }
        public List<SelectListItem> BookCopies { get; set; }


        public IFormFile File { get; set; }


        [Required]
        public BookCost PriceOption { get; set; }
        public List<SelectListItem> PriceOptions { get; set; }

        public double Cost { get; set; }


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
            BookCopies = new List<SelectListItem>();
            BookCopies.Add(new SelectListItem
            {
                Value = ((int)BookCopy.HardCopy).ToString(),
                Text = BookCopy.HardCopy.ToString()
            });
            BookCopies.Add(new SelectListItem
            {
                Value = ((int)BookCopy.SoftCopy).ToString(),
                Text = BookCopy.SoftCopy.ToString()
            });


            PriceOptions = new List<SelectListItem>();
            PriceOptions.Add(new SelectListItem
            {
                Value = ((int)BookCost.Free).ToString(),
                Text = BookCost.Free.ToString()
            });
            PriceOptions.Add(new SelectListItem
            {
                Value = ((int)BookCost.Swap).ToString(),
                Text = BookCost.Swap.ToString()
            });
            PriceOptions.Add(new SelectListItem
            {
                Value = ((int)BookCost.Rent).ToString(),
                Text = BookCost.Rent.ToString()
            });
            PriceOptions.Add(new SelectListItem
            {
                Value = ((int)BookCost.Buy).ToString(),
                Text = BookCost.Buy.ToString()
            });
        }

        
    }
}
