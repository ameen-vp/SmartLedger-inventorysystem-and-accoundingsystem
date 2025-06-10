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
    public interface IproductService
    {
        Task<Apiresponse<List<ProductAdddto>>> AddProduct(ProductAdddto productAdddto);

        Task<Apiresponse<List<Productviewdto>>> GetAllproducts();

        Task<Apiresponse<Productviewdto>> Getproductbyid(int id);


        Task<Apiresponse<productupdatedto>> Updateproducts(int id, productupdatedto productupdatedto);
        Task<Apiresponse<string>> Deleteproduct(int id);
    }
}
