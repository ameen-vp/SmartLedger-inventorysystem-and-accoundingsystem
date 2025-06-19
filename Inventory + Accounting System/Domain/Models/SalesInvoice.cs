using Domain.Enum;
using System;
using System.Collections.Generic;

namespace Domain.Models
{
    public class SalesInvoice
    {
        public int Id { get; set; }

        public string InvoiceNumber { get; set; } 
        public DateTime InvoiceDate { get; set; }

        public int CustomerId { get; set; }

        public InvoiceStatus Status { get; set; } = InvoiceStatus.PENDING; 

        public Costomer Customer { get; set; }  
        public decimal TotalAmount { get; set; }

        public string Remarks { get; set; }

       
        public ICollection<SalesItems> SalesItems { get; set; }
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

        public SalesInvoice SalesInvoice { get; set; }  
    }
}
