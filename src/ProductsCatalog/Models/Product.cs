using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ProductsCatalog.Models
{
    public class Product
    {
        public int ProductId { get; set; }
        [Required]
        [MaxLength(512)]
        public string Name { get; set; }
        [Required]
        [MaxLength(2000)]
        public string BriefDescription { get; set; }
        
        public decimal Cost { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        public bool IsActive { get; set; }
    }
}
