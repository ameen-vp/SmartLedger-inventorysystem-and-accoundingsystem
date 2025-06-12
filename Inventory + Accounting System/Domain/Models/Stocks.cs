using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
  public  class Stocks
    {
            public int Id { get; set; }
            public int ProductId { get; set; }
            public int Quantity { get; set; }
            public DateTime Date { get; set; } = DateTime.UtcNow;

            public Product product { get; set; }
      

    }
}
