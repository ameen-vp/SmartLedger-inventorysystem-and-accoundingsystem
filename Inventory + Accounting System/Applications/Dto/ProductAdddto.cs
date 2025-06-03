using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Applications.Dto
{
   public class ProductAdddto
    {
        public string Name { get; set; }
        public string SKU { get; set; } 
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public string Category { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
