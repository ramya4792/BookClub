
using System.ComponentModel.DataAnnotations.Schema;

namespace BookClub.Models
{
    public class Book
    {
        public int ID { get; set; }

        public int UserId { get; set; }
        
        [ForeignKey("UserId")]
        public ApplicationUser User { get; set; }
        
        public string Title { get; set; }
        public string Author { get; set; }
        public string Description { get; set; }

        
        public int BookCategoryID { get; set; }
        public BookCategory BookCategory { get; set; }
        
        public string CoverPage { get; set; }
        public BookCopy Copy { get; set; }        
        
        public string File { get; set; }

        public BookCost PriceOption { get; set; }
        public double Price { get; set; }

    }
}
