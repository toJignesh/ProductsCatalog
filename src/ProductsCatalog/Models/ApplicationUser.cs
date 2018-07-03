using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ProductsCatalog.Models
{
    public class ApplicationUser:IdentityUser
    {
        [MaxLength(100)]
        public string Name { get; set; }

        [MaxLength(4000)]
        public string SavedSearches { get; set; }
    }
}
