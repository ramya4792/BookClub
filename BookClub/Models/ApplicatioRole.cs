using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookClub.Models
{
    public class ApplicatioRole : IdentityRole<int>
    {
        public ApplicatioRole() { }
        public ApplicatioRole(string name)
        {
            Name = name;
        }

    }
}
