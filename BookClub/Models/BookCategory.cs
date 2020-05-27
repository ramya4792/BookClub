using System.Collections.Generic;

namespace BookClub.Models
{
    public class BookCategory
    {
        public int ID { get; set; }
        public string Subject { get; set; }
        public IList<Book> Books { get; set; }
    }
}
