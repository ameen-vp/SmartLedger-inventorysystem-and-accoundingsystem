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
   public interface ICostomerService
    {
        Task<Apiresponse<string>> AddCostomer(CostomerDto costomerDto);

        Task<Apiresponse<List<Costomer>>> Get();
    }
}
