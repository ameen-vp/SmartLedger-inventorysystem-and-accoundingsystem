using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
   public class PurchaseItems
    {
        public int Id { get; set; }

        public int PurchaseInvoiceId { get; set; }

        public PurchaseInvoice PurchaseInvoice { get; set; }

        public int ProductId { get; set; }

        public Product product { get; set; }
        public int Quantity { get; set; }

        public decimal UnitPrice { get; set; }

        public decimal GSTPercentage { get; set; }

        public decimal GSTAmount { get; set; }

        public decimal ToTalAmount { get; set; }
    }
}
