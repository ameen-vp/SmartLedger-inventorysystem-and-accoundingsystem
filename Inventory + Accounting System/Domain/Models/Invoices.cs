using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
   public class Invoices
    {
        public int InvoicesID { get; set;}

        public int CostmerId { get; set; }

        public DateTime dateTime { get; set; } = DateTime.Now;

        public decimal TotalAmount { get; set; }

        public int CreatedBy { get; set; }
    }
}
