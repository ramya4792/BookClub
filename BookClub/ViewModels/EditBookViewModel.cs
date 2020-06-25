using BookClub.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

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
