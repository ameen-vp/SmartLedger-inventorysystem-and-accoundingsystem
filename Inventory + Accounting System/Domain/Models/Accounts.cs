using Domain.Enum;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
   public  class Accounts
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public AccountType Type { get; set; }

        public decimal Balance { get; set; }

        public ICollection<LedgerEntry> DebitEntrys { get; set; }
        public ICollection<LedgerEntry> CreditEntrys { get; set; }

        public ICollection<Costomer> costomers { get; set; }
    }
    public class JournalEntry
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public string Narration { get; set; }

        public ICollection<JournalLine> journalLines { get; set; }
    }
    public class JournalLine
    {
        public int Id { get; set; }

        public int JournalEntryId { get; set; }
        [ForeignKey("JournalEntryId")]
        public JournalEntry JournalEntry { get; set; }

        public int AccountId { get; set; }

        [ForeignKey("AccountId")]
        public Accounts Account { get; set; }

        public decimal Debit { get; set; }
        public decimal Credit { get; set; }
    }
    public class LedgerEntry
    {
        public int Id { get; set; }
        public DateTime EntryDate { get; set; } = DateTime.Now;

        public int? SalesInvoiceId { get; set; }

        public SalesInvoice SalesInvoice { get; set; }
        public string Description { get; set; }

        public int DebitAccountId { get; set; }
   
        public Accounts DebitAccount { get; set; }

        
        public int CreditAccountId { get; set; }
      
        public Accounts CreditAccount { get; set; }

        public decimal Amount { get; set; }
    }

}
