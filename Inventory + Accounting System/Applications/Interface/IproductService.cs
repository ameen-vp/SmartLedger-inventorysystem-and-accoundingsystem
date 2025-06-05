using Applications.ApiResponse;
using Applications.Dto;
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
    }
}
