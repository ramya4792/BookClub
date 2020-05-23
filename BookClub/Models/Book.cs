using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookClub.Models
{
    public class Book
    {
        public int ID { get; set; }
        
        public string Title { get; set; }
        public string Author { get; set; }
        public string Description { get; set; }
        public int BookCategoryID { get; set; }
        public BookCategory BookCategory { get; set; }
        
        

    }
}
