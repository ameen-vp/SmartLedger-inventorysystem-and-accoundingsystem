using Applications.ApiResponse;
using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Applications.Interface
{
   public interface ICatRepo
    {
        Task AddCatagory(Category category);

        Task<bool> Catagoryexit(string name);

        Task<List<Category>> GetAllCategorys();

        Task<bool> Deletecatagoeys(int id);

     

    }
}
