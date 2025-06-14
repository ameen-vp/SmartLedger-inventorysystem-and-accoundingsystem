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

        public int SupplierId { get; set; }
     
        public decimal SellingPrice { get; set; }

        public ICollection<StockTransactionViewDto> StockTransactionViewDtos { get; set; }

    }
}
