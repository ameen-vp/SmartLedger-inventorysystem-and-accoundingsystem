using Applications.ApiResponse;
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
    public class StockRepo : IStockRepo
    {
        private readonly AppDbContext _appDbContext;

        public StockRepo(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public async Task Addstock(Stocks stocks)
        {
            await _appDbContext.Stocks.AddAsync(stocks);
            await _appDbContext.SaveChangesAsync();
        }

        public async Task<Stocks> GetByProductId(int id)
        {
            return await _appDbContext.Stocks.FirstOrDefaultAsync(x => x.Id == id);
        }
        public async Task<List<Stocks>> Getstock()
        {
            return await _appDbContext.Stocks.ToListAsync();
        }
        public async Task<Stocks> FindId(int id)
        {
            return await _appDbContext.Stocks.FindAsync(id);
        }
        public async Task<bool> DeleteStock(int id)
        {
            var delete = await _appDbContext.Stocks.Where(x => x.Id == id).ExecuteDeleteAsync();
            return delete > 0;

        }
        public async Task UpdateStock(Stocks stocks)
        {
            _appDbContext.Stocks.Update(stocks);
            await _appDbContext.SaveChangesAsync();
        }
       public async Task<Stocks> FindproductId(int productId)
        {
            return await _appDbContext.Stocks.FirstOrDefaultAsync(x => x.ProductId == productId);

        }
        public async Task<Stocks> GetstockId(int ProductId)
        {
            return await _appDbContext.Stocks .Include(s => s.stockTransactions)
                .FirstOrDefaultAsync(s => s.ProductId == ProductId);
        }
    }
}
