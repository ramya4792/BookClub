using Stripe;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace BookClub.ViewModels
{
    public class ChargeViewModel
    {
        public string ChargeId { get; set; }
        public double Amount { get; set; }

    }
}
