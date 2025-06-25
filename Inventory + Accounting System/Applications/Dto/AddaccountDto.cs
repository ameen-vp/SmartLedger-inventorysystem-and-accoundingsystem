using Domain.Enum;
using Domain.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Applications.Dto
{
   public class AddaccountDto
    {
        public string Name { get; set; }
        public AccountType Type { get; set; }

        public decimal Balance { get; set; }
    }
    public class AddLedgerDto
    {
        public int DebitAccountId { get; set; }
        public int CreditAccountId { get; set; }
        public string Description { get; set; }

        public decimal Amount { get; set; }
    }
}
