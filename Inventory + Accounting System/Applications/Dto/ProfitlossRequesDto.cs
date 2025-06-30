using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Applications.Dto
{
   public class ProfitlossRequesDto
    {
        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }
    }
    public class ProfitloassresponseDto
    {
        [Required]
        public decimal TotalSales { get; set; }

        [Required]
        public decimal TotalPurchases { get; set; }

        [Required]
        public decimal OtherIncome { get; set; }

        [Required]
        public decimal OtherExpenses { get; set; }

        public decimal GrossProfit => TotalSales - TotalPurchases;
        public decimal NetProfit => GrossProfit + OtherIncome - OtherExpenses;
    }
}
