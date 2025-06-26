using Applications.Dto;
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
   public  class LedgerREpo : ILedgerRepo
    {
        private readonly AppDbContext _appDbContext;

        public LedgerREpo(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }
        public async Task AddEntry(LedgerEntry ledgerEntry)
        {
            try
            {
                await _appDbContext.LedgerEntries.AddAsync(ledgerEntry);
                await _appDbContext.SaveChangesAsync();
            }catch(DbUpdateException ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public async Task<Accounts> GetdebitId(int id)
        {
            try
            {
                return await _appDbContext.Accounts.FirstOrDefaultAsync(x => x.Id == id);
            }catch(DbUpdateException ex)
            {
                throw new DbUpdateException(ex.Message);
            }

        }
       public async Task<Accounts> GetcreditId(int id)
        {
            try
            {
                return await _appDbContext.Accounts.FirstOrDefaultAsync(x => x.Id == id);
            }catch(DbUpdateException ex)
            {
                throw new DbUpdateException(ex.Message);
            }
        }
        public async Task Update(Accounts accounts)
        {
            try
            {
                 _appDbContext.Accounts.Update(accounts);
                await _appDbContext.SaveChangesAsync();
            }
            catch (DbUpdateException ex)
            {
                throw new DbUpdateException
                    (ex.Message);
            }
        }
        public async Task<bool> Delete(int id)
        {
            try
            {
                var del = await _appDbContext.LedgerEntries.FindAsync(id);
                if(del == null)
                {
                    return false;
                }
                _appDbContext.Remove(del);
               await _appDbContext.SaveChangesAsync();
                return true;
            }catch(DbUpdateException ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
