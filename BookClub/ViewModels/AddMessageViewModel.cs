using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BookClub.ViewModels
{
    public class AddMessageViewModel
    {
        [Required]
        public string Message { get; set; }
        public DateTime Date { get; set; }
        [Required]
        public int ReceiverId { get; set; }
        public AddMessageViewModel()
        {
        }
    }
}
