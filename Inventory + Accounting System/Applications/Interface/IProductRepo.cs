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
    public interface IProductRepo
    {
        Task Addproduct(Product product);

         Task<Product> Exits(string name);

        Task UpdateProduct(Product product);

        Task<bool> Categorycheck(int id);
    }
}
