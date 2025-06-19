using Domain.Enum;
using Domain.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Applications.Dto
{
   public class SalesInvoicesDto
    {
        public int Id { get; set; }

        public string InvoiceNumber { get; set; }
        public DateTime InvoiceDate { get; set; }

        public int CustomerId { get; set; }

        public InvoiceStatus Status { get; set; } = InvoiceStatus.PENDING;

  
        public decimal TotalAmount { get; set; }

        public string Remarks { get; set; }


        public ICollection<SalesItems> SalesItems { get; set; }
    }
    public class ADDSalesInvoice
    {


        [Required]
        public DateTime InvoiceDate { get; set; }

        [Required]
        public int CustomerId { get; set; }

        public string Remarks { get; set; }

        [Required]
        public List<AddSalesItemDto> SalesItems { get; set; }
    }
    public class AddSalesItemDto
    {
        [Required]
        public int ProductId { get; set; }

        [Required]
        public int Quantity { get; set; }

 
        public decimal Discount { get; set; } = 0;

    }
    public class UpdateStatusDto
    {    
        public int InvoiceId { get; set; }
        public  InvoiceStatus InvoiceStatus { get; set; }
    }
}
