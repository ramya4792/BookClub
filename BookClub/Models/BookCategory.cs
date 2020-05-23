using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookClub.Models
{
    public class BookCategory
    {
        public int ID { get; set; }
        public string Subject { get; set; }
        public IList<Book> Books { get; set; }
    }
}
