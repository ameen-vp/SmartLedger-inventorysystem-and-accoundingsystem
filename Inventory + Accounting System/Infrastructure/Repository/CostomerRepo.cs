using Applications.Interface;
using Domain.Models;
using Infrastructure.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repository
{
   public class CostomerRepo : ICostmerRepo
    {
        private readonly AppDbContext _appDbContext;
        public CostomerRepo(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }
       public async Task<bool> AddCostomers(Costomer costomer)
        {
            try
            {
                _appDbContext.Costomer.Add(costomer);
                return await _appDbContext.SaveChangesAsync() > 0;
            }catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
       public async Task<List<Costomer>> GetCostomers()
        {
            return await _appDbContext.Costomer.ToListAsync();
        }
        public async Task<bool> DeleteCostomers(int id)
        {
            try
            {
                var cos = await _appDbContext.Costomer.FindAsync(id);
                if (cos == null)
                {
                    return false; 
                }
                _appDbContext.Costomer.Remove(cos);
                await _appDbContext.SaveChangesAsync();
                return true;
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public async Task<bool> Update(Costomer costomer)
        {
            var existingCustomer = await _appDbContext.Costomer.FirstOrDefaultAsync(c => c.CostomerId == costomer.CostomerId);
            if (existingCustomer == null)
                return false;

            existingCustomer.AccountId = costomer.AccountId;
            

            await _appDbContext.SaveChangesAsync();
            return true;
        }
    }
}
