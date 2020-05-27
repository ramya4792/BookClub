using System.ComponentModel.DataAnnotations;

namespace BookClub.ViewModels
{
    public class AddBookCategoryViewModel
    {
        [Required]
        public string Subject { get; set; }
    }
}
