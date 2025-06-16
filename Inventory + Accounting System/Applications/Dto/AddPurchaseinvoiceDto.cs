using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Applications.Dto
{
   public class AddPurchaseinvoiceDto
    {
        public int VendorId { get; set; }
        public DateTime InvoiceDate { get; set; }

              
        public List<PurchaseItemCreateDto> PurchaseItems { get; set; }
    }
}
