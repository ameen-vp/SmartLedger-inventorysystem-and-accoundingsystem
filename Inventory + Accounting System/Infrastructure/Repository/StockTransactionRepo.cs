using Applications.ApiResponse;
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
  public class StockTransactionRepo : IStockTransactionsRepo
    {
        private readonly AppDbContext _appDbContext;

        public StockTransactionRepo(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }
       public async Task AddTransaction(StockTransactions stockTransaction)
        {
            await _appDbContext.stockTransactions.AddAsync(stockTransaction);
            await _appDbContext.SaveChangesAsync();
        }
        public async Task<Product> GetProductId(int productId)
        {
            return await _appDbContext.Products.FindAsync(productId);
        }
        public async Task<Stocks> GetStockByProductIdAsync(int ProductId)
        {
            return await _appDbContext.Stocks.Include(x => x.stockTransactions).
                FirstOrDefaultAsync(x => x.ProductId == ProductId);
        }
       public async Task<List<StockTransactions>> Gettransactions()
        {
            return  await _appDbContext.stockTransactions
                .Include(x => x.product)
                .ToListAsync();
            
        }
    }
}
