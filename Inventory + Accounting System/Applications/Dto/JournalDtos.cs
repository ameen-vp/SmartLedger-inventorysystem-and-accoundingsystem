using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Applications.Dto
{
   public class JournalDtos
    {
        public DateTime Date { get; set; }
        public string Narration { get; set; }

        public ICollection<JournalLineDto> journalLines { get; set; }
    }
    public class JournalLineDto
    {
        public int AccountId { get; set; }
        public decimal Debit { get; set; }
        public decimal Credit { get; set; }
    }
    public class JournalViewDto
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public string Narration { get; set; }

        public ICollection<JournalLineDto> journalLines { get; set; }
    }
}
