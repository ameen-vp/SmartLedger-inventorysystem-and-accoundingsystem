using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
   public class Costomer
    {
        public int CostomerId { get; set; }
        [Required(ErrorMessage = "Name is required ")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Phone number is required")]
        [MaxLength(10)]
        public string Phone { get; set; }

        public ICollection<SalesInvoice> SalesInvoices { get; set; }
    }
}
