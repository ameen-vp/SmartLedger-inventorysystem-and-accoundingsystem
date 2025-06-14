using Domain.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Applications.Dto
{
   public class AddStockTransactionDto
    {
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public Transactiontype TransactionType { get; set; }
        public DateTime TransactionDate { get; set; } = DateTime.UtcNow;
    }
}
