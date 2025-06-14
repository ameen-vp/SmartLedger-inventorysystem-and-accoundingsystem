using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
 public class Product
    {
        public int Id { get; set; }
        public string ProductName { get; set; }
        public string SKU { get; set; } // Unique Stock Keeping Unit
        public int CategoryId { get; set; }
        public int SupplierId { get; set; }

        public decimal PurchasePrice { get; set; }
        public decimal SellingPrice { get; set; }
        //public string CategoryName { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;

        public Category category { get; set; }

       public Stocks Stocks { get; set; }

        public ICollection<StockTransactions> StockTransactions { get; set; } 
    }
}
