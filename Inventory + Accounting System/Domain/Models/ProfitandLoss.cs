using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
   public class ProfitandLoss
    {
        public int Id { get; set; }
        public DateOnly StartDate { get; set; }

        public DateOnly EndDate { get; set; }

        public decimal TotalSales { get; set; }

        public decimal TotalPurchases { get; set; }

        public decimal OtherIncome { get; set; }

        public decimal OtherExpenses { get; set; }
        public decimal GrossProfit { get;  set; }
        public decimal NetProfit { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}
