using Applications.Dto;
using Applications.Interface;
using Domain.Enum;
using Domain.Models;
using Infrastructure.Contexts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.WebSockets;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repository
{


   public class ProfitAndLossRepo : IProfitlossRepo
    {
        private readonly AppDbContext _appDbContext;
        private readonly IAccountRepo _accountRepo;

        public ProfitAndLossRepo(AppDbContext appDbContext,IAccountRepo accountRepo)
        {
            _appDbContext = appDbContext;
            _accountRepo = accountRepo;
        }
       public async Task<ProfitandLoss> CalculateProfitandloss(DateOnly startDate, DateOnly endDate)
        {
            var start = startDate.ToDateTime(TimeOnly.MinValue); 
            var end = endDate.ToDateTime(TimeOnly.MaxValue);     

            var journal = await _appDbContext.JournalLines
                .Include(x => x.Account)
                .Include(x => x.JournalEntry)
                .Where(x => x.JournalEntry.Date >= start && x.JournalEntry.Date <= end)
                .ToListAsync();



            var totalexpence = journal.Where(x => x.Account.Type == Domain.Enum.AccountType.EXPENSE)
                                      .Sum(x => x.Debit);
            var totalincome = journal.Where(x => x.Account.Type == Domain.Enum.AccountType.INCOME)
                                .Sum(x => x.Credit);
            decimal totalSales = journal
         .Where(x => x.Account.Type == AccountType.INCOME && x.Account.Name.Contains("Sales"))
         .Sum(x => x.Credit);

            var purchaseAccountId = await _accountRepo.GetAccountsIdByPurchase();
            var totalPurchases = journal
                .Where(x => x.AccountId == purchaseAccountId)
                .Sum(x => x.Debit);

            var pl = new ProfitandLoss
            {
                StartDate = startDate,
                EndDate = endDate,
                OtherIncome = totalincome,
                OtherExpenses = totalexpence,
                TotalPurchases = totalPurchases,
                TotalSales = totalSales
            };
            pl.GrossProfit = pl.TotalSales - pl.TotalPurchases;
            pl.NetProfit = pl.GrossProfit + pl.OtherIncome - pl.OtherExpenses;

            return pl;
        }
    }
}
