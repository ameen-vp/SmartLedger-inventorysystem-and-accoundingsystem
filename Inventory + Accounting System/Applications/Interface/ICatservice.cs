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
   public interface ICatservice
    {
        Task<Apiresponse<CategoryAdddto>> AddCategory(CategoryAdddto categoryAdddto);

        Task<Apiresponse<List<Category>>> GetAllCategorys();

        Task<Apiresponse<string>> Deletecategorys(int id);
    }
}
