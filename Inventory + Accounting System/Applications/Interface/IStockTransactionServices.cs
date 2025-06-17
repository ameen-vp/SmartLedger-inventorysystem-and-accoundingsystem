using Applications.ApiResponse;
using Applications.Dto;
using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Applications.Interface
{
   public interface IStockTransactionServices
    {
        Task<Apiresponse<AddStockTransactionDto>> AddTransactions(AddStockTransactionDto addStockTransactionDto);

        Task<Apiresponse<List<StockTransactionViewDto>>> Get();

        Task<Apiresponse<string>> Delete(int id);
    }
}
