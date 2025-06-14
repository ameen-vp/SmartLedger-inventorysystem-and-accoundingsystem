using Applications.ApiResponse;
using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Applications.Interface
{
   public interface IStockTransactionsRepo
    {
        Task AddTransaction(StockTransactions stockTransaction);

        Task<Product> GetProductId(int productId);

         Task<Stocks> GetStockByProductIdAsync(int ProductId);

        Task<List<StockTransactions>> Gettransactions();
    }
}
