using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookClub.Models
{
    public class AddMessage
    {
        public int ID { get; set; }
        public string  Message { get; set; }
        public ApplicationUser User { get; set; }
        public int UserId { get; set; }
        public int ReceiverId { get; set; }

        public DateTime Date { get; set; }
    }
}
