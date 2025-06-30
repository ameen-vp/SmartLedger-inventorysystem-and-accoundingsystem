using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Applications.Dto
{
   public class VendorAdddto
    {
        [Required(ErrorMessage = "Name is reqiured")]
        [MaxLength(30)]
        public string VendorName { get; set; }
        [Required(ErrorMessage = "Phone Number is required")]
        [RegularExpression(@"^\d{10}",ErrorMessage = "phone number Must be !0 digits")]
        public string Phone { get; set; }

        public string ? place { get; set; }

        public int? AccountId { get; set; }

    }
    public class vendeorviewDto
    {
        public int VendorId { get; set; }
        public string VendorName { get; set; }

        public string Phone { get; set; }

        public string? place { get; set; }

        public ICollection<PurchaseinvoiceDto> purchaseinvoiceDtos { get; set; }

    }
}
