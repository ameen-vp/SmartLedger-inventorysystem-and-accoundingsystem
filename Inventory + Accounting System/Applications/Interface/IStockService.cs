using Applications.ApiResponse;
using Applications.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Applications.Interface
{
   public interface IStockService
    {
        Task<Apiresponse<StockAdddto>> Addstock(StockAdddto stockAdddto);

        Task<Apiresponse<List<StockviewDto>>> Get();

        Task<Apiresponse<StockviewDto>> GetById(int id);

        Task<Apiresponse<string>> Delete(int id);

        Task<Apiresponse<Stockupdatedto>> Updatestock(Stockupdatedto stockupdatedto);
    }
}
