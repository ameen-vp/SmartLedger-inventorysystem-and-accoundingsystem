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
       public async  Task Addacoount(Accounts accounts)
        {
             await _appDbContext.Accounts.AddAsync(accounts);
            await _appDbContext.SaveChangesAsync();
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
    }
}
