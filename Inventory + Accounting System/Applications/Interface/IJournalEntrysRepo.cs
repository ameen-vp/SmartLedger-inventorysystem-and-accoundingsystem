using Applications.ApiResponse;
using Applications.Dto;
using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Applications.Interface
{
  public interface IJournalEntrysRepo
    {
        Task<JournalEntry> AddJournalEntrys(JournalEntry entry);

        Task<List<JournalEntry>>Get();

        Task AddJournalLines(List<JournalLine> lines);

    }
    public interface IJournalentryService
    {
        Task<Apiresponse<JournalEntry>> AddJournalEntrys(JournalDtos journalDtos);

        Task<Apiresponse<List<JournalViewDto>>> Get();
    }
}
