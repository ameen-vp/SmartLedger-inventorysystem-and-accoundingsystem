using Applications.Interface;
using Domain.Models;
using Infrastructure.Contexts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repository
{
   public class JournalEntryRepo : IJournalEntrysRepo
    {
        private readonly AppDbContext _appDbContext;

        public JournalEntryRepo(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }
     
        public async Task<List<JournalEntry>>Get()
        {
            return await _appDbContext.journalEntries.Include(x => x.journalLines).ToListAsync();
        }
        public async Task<JournalEntry> AddJournalEntrys(JournalEntry entry)
        {
            _appDbContext.journalEntries.Add(entry);
            await _appDbContext.SaveChangesAsync();
            return entry; 
        }
        public async Task AddJournalLines(List<JournalLine> lines)
        {
            _appDbContext.JournalLines.AddRange(lines);
            await _appDbContext.SaveChangesAsync();
        }

    }
}
