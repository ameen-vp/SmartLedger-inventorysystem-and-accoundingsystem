using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Applications.Dto
{
   public class CostomerDto
    {
        [Required(ErrorMessage = "Name is required ")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Phone number is required")]
        [MaxLength(10)]
        public string Phone { get; set; }
    }
}
