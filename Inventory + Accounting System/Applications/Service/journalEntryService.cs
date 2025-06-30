using Applications.ApiResponse;
using Applications.Dto;
using Applications.Interface;
using AutoMapper;
using Domain.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace Applications.Service
{
   public class journalEntryService : IJournalentryService
    {
        private readonly IJournalEntrysRepo _journalEntrysRepo;
        private readonly IMapper _mapper;
        private readonly IAccountRepo _accountrepo;
        private readonly ILedgerRepo _ledgerrepo;
      

        public journalEntryService (IJournalEntrysRepo journalEntrysRepo, IMapper mapper ,IAccountRepo accountRepo,ILedgerRepo ledgerRepo)
        {
            _journalEntrysRepo = journalEntrysRepo;
            _mapper = mapper;
            _accountrepo = accountRepo;
            _ledgerrepo = ledgerRepo;
        }

        public async Task<Apiresponse<JournalEntry>> AddJournalEntrys(JournalDtos journalDtos)
        {
            try
            {
                if (journalDtos == null || journalDtos.journalLines.Count() < 2)
                {
                    return new Apiresponse<JournalEntry> { Message = "Journal entry must have at least two lines. ", Statuscode = 400 };
                }
                ;
                decimal totaldebit = journalDtos.journalLines.Sum(x => x.Debit);
                decimal totalcredit = journalDtos.journalLines.Sum(x => x.Credit);

                if (totaldebit != totalcredit)
                {
                    return new Apiresponse<JournalEntry> { Message = "Journal entry is not balanced (Dr ≠ Cr)", Statuscode = 400 };
                }

                var accoumids = journalDtos.journalLines.Select(x => x.AccountId).Distinct().ToList();

                var exit = await _accountrepo.GetAccountsByIdsAsync(accoumids);

                if (accoumids.Count != exit.Count)
                {
                    return new Apiresponse<JournalEntry>
                    {
                        Message = "One or more accounts are invalid.",
                        Statuscode = 400
                    };
                }
                var jounalentry = new JournalEntry
                {
                    Date = DateTime.UtcNow,
                    Narration = journalDtos.Narration,
                    journalLines = journalDtos.journalLines.Select(x => new JournalLine
                    {
                        AccountId = x.AccountId,
                        Debit = x.Debit,
                        Credit = x.Credit
                    }).ToList()
                };
                await _journalEntrysRepo.AddJournalEntrys(jounalentry);

                foreach (var line in journalDtos.journalLines)
                {
                    LedgerEntry ledger = new LedgerEntry
                    {
                        EntryDate = DateOnly.FromDateTime(journalDtos.Date),
                        Description = journalDtos.Narration,
                        Amount = line.Debit > 0 ? line.Debit : line.Credit,
                        DebitAccountId = line.Debit > 0 ? line.AccountId : null,
                        CreditAccountId = line.Credit > 0 ? line.AccountId : null,
                    };
                    await _ledgerrepo.AddEntry(ledger);
                }
                return new Apiresponse<JournalEntry>
                {
                    Statuscode = 200,
                    Message = "Journal Entry Added SucsessFully",
                    Success = true
                };
            }
            catch (DbUpdateException ex)
            {
                throw new DbUpdateException(ex.Message);
            }  }
        public async Task<Apiresponse<List<JournalViewDto>>> Get()
        {
            var entry = await _journalEntrysRepo.Get();
            //var map = _mapper.Map<JournalViewDto>(entry);
            if(entry == null || entry.Count() == 0)
            {
                return new  Apiresponse<List<JournalViewDto>>
                {
                    Message = "Jounrnal Entrys Not found",
                    Statuscode = 404,
                    Success = false,
                };
            }
            var journal = entry.Select(x => new JournalViewDto
            {
                Date = x.Date,
                Narration = x.Narration,
                journalLines = x.journalLines.Select(x => new JournalLineDto
                {
                    AccountId = x.AccountId,
                    Debit = x.Debit,
                    Credit = x.Credit

                }).ToList()
            }).ToList();
            return new Apiresponse<List<JournalViewDto>> {
               
                Data = journal,
                Statuscode = 200,
                Success = true
            };
        }
    }
}
