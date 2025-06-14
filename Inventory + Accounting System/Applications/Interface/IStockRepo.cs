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
    public interface IStockRepo
    {
        Task  Addstock(Stocks stocks);

        Task<Stocks> GetByProductId(int id);

        Task<List<Stocks>> Getstock();

        Task<Stocks> FindId(int id);

        Task<bool>  DeleteStock(int id);

        Task UpdateStock(Stocks stocks);

        Task<Stocks> FindproductId(int productId);

        Task<Stocks> GetstockId(int ProsuctId);

        

    }
}
