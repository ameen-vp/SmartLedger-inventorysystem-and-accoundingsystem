using Applications.Interface;
using Domain.Enum;
using Domain.Models;
using Infrastructure.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repository
{
   public class AccountRepo : IAccountRepo
    {
        private readonly AppDbContext _appDbContext;
        public AccountRepo(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }
        public async Task<Accounts> Addacoount(Accounts accounts)
        {
            await _appDbContext.Accounts.AddAsync(accounts);
            await _appDbContext.SaveChangesAsync();
            return accounts;  
        }
        public async Task<List<Accounts>> Get()
        {
            return await _appDbContext.Accounts.ToListAsync();
        }
        public  IEnumerable<Accounts> GetTypes(AccountType type)
        {
            return  _appDbContext.Accounts.Where(x => x.Type == type).ToList();
        }
        public async Task<bool> Delete(int id)
        {
            var check = await _appDbContext.Accounts.FindAsync(id);
            if (check == null)
            {
                return false; 
            }
             _appDbContext.Accounts.Remove(check);
            await _appDbContext.SaveChangesAsync();
            return true;
        }
        public async Task<int> GetcustomerId(int id)
        {
            var customer = await _appDbContext.Costomer.FirstOrDefaultAsync(c => c.CostomerId == id);
            if (customer == null) throw new Exception("Customer not found");

  
            var accountName = $"Customer-{customer.Name}";

            var account = await _appDbContext.Accounts.FirstOrDefaultAsync(a => a.Name == accountName);
            if (account == null) throw new Exception("Customer account not found");

            return account.Id;
        }

      public async Task<int> GetAccountsId( )

        {
            var account = await _appDbContext.Accounts.FirstOrDefaultAsync(a => a.Name.ToLower() == "sales");
            if (account == null) throw new Exception("Sales revenue account not found");

            return account.Id;
        }
    }
}
