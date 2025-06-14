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

        Task<bool> Exits(string name);

        Task UpdateProduct(Product product );

        Task<bool> Categorycheck(int id);

        Task<List<Product>> GetAllProducts();
        Task<Product> GetproductbyId(int id);

        Task<bool> productexit(int id);

        Task<bool> Idcheck(int id);
        
        Task<bool> DeleteProducts(int id);

        Task<List<Product>> GetProductviewdtos();


    }
}
