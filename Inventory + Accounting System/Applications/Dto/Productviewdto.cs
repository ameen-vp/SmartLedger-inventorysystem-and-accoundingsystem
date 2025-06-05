using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Applications.Dto
{
  public  class Productviewdto
    {
        public int Id { get; set; }
        public string ProductName { get; set; }
        public string SKU { get; set; }
        public string CategoryName { get; set; }
        public string SupplierName { get; set; }
        public int Quantity { get; set; }
        public decimal SellingPrice { get; set; }

    }
}
