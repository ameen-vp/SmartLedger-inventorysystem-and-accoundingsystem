using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Applications.Dto
{
   public class PurchaseinvoiceDto
    {
        public int Id { get; set; }

        public int VentorsId { get; set; }


        public string InvoiceNumber { get; set; }

        public DateTime Date { get; set; } = DateTime.UtcNow;

        public decimal TotalAmount { get; set; }

        public decimal GST { get; set; }

        public decimal GrantToTal { get; set; }
        public ICollection<PurchaseItemDto> purchaseItem{ get; set; }
    }
    public class PurchaseItemDto
    {
        public int ProductId { get; set; }

     
        public int Quantity { get; set; }

        public decimal UnitPrice { get; set; }

        public decimal GSTPercentage { get; set; }

        //public decimal GSTAmount { get; set; }

        //public decimal ToTalAmount { get; set; }
    }
    public class CreateInvoiceDraftDto
    {
        public int VendorId { get; set; }
        public DateTime InvoiceDate { get; set; }
    }

}
