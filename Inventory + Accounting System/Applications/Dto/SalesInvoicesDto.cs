using Domain.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Applications.Dto
{
   public class SalesInvoicesDto
    {

    }
    public class ADDSalesInvoice
    {
        public DateTime InvoiceDate { get; set; }
        public int CustomerId { get; set; }
        public InvoiceStatus Status { get; set; } = InvoiceStatus.PENDING;
    }
}
