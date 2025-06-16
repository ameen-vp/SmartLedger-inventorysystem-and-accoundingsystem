using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
  public  class PurchaseInvoice
    {
        public int Id { get; set; }

        public int VentorsId { get; set; }

        public Vendor Vendor { get; set; }

        public string InvoiceNumber { get; set; }

        public DateTime Date { get; set; } = DateTime.UtcNow;

        public decimal TotalAmount { get; set; }

        public decimal GST { get; set; }

        public decimal GrantToTal { get; set; }
        public ICollection<PurchaseItems> purchaseItems { get; set; }


    }
}
