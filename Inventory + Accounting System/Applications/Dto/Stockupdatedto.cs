using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Applications.Dto
{
   public class Stockupdatedto
    {
        public int ProductId { get; set; }

        public int Quantity { get; set; }

        public DateTime? LastUpdated { get; set; } = DateTime.UtcNow;
    }
}
