﻿using Domain.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Applications.Dto
{
   public class StockTransactionViewDto
    {
        public int Id { get; set; }
        public int ProductId { get; set; }

        public string Productname { get; set; }
        public int Quantity { get; set; }
        public Transactiontype TransactionType { get; set; }
        public DateTime TransactionDate { get; set; }
        public int StockId { get; set; }
    }
}
