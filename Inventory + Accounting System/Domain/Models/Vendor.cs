using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
   public class Vendor
    {
        public int VendorId { get; set; }

        [Required(ErrorMessage = "Name is reqiured")]
        [MaxLength(30)]
        public string VendorName { get; set; }
        [Phone]
        public string Phone { get; set; }

        public string ? place { get; set; }

    }
}
