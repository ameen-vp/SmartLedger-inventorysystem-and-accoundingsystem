using Domain.Enum;
using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Domain.Models
{
    public class SalesInvoice
    {
        public int Id { get; set; }

        public string InvoiceNumber { get; set; } 
        public DateTime InvoiceDate { get; set; }

        public int CustomerId { get; set; }

        public InvoiceStatus Status { get; set; } = InvoiceStatus.PAID; 

        public Costomer Customer { get; set; }  
        public decimal TotalAmount { get; set; }

        public string Remarks { get; set; }

       
        public ICollection<SalesItems> SalesItems { get; set; }
        public ICollection<LedgerEntry> LedgerEntries { get; set; } 
    }

    public class SalesItems
    {
        public int Id { get; set; }

        public int ProductId { get; set; } 

        public Product Product { get; set; }  

        public int Quantity { get; set; }

        public decimal UNITPrice { get; set; } 

        public decimal Discount { get; set; } = 0;  

        public decimal Gst { get; set; } = 0;  

        public decimal TotalPrice { get; set; } 

        public int SalesInvoiceId { get; set; }
        [JsonIgnore]
        public SalesInvoice SalesInvoice { get; set; }  
    }
}
