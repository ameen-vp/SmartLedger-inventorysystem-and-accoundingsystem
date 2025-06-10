using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Applications.Dto
{
   public class StockAdddto
    {   
        [Required(ErrorMessage ="Product Id is required")]
        public int ProductId { get; set; }

        [Required(ErrorMessage = "Type  is required")]
        public string Type { get; set; }

        [Required(ErrorMessage = "Quantity is required")]
        public int Quantity { get; set; }
    }
}
