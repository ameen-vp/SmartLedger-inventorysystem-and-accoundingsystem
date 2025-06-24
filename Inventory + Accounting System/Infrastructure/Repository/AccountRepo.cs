using Applications.Interface;
using Domain.Models;
using Infrastructure.Contexts;
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
    }
}
