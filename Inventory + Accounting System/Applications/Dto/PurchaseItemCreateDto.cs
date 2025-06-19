using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Applications.Dto
{
   public class PurchaseItemCreateDto
    {
        public int ProductId { get; set; }

        public string ProductName { get; set; }
        public string SKU { get; set; } 
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal GSTPercent { get; set; }

        //public decimal GSTAmount { get; set; }
    }
}
