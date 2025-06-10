using Applications.ApiResponse;
using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Applications.Interface
{
    public interface ICostmerRepo
    {
        Task<bool> AddCostomers(Costomer costomer);

        Task<List<Costomer>> GetCostomers();

        Task<bool> DeleteCostomers(int id);

        Task<bool> Chekid(int id);

    }
    
}
