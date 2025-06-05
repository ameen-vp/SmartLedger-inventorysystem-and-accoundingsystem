using Applications.ApiResponse;
using Applications.Dto;
using Applications.Interface;
using AutoMapper;
using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Applications.Service
{
   public class ProductService : IproductService
    {
        private readonly IProductRepo _productRepo;
        private readonly IMapper _mapper;
        public ProductService(IProductRepo productRepo,IMapper mapper)
        {
            _productRepo = productRepo;
            _mapper = mapper;
        }
      public async  Task<Apiresponse<List<ProductAdddto>>> AddProduct(ProductAdddto productAdddto)
        {
            try
            {
                var exitproduct =await _productRepo.Exits(productAdddto.ProductName);
               

                if (exitproduct != null)
                {
                    //Console.WriteLine($"Existing quantity: {exitproduct.Quantity}");
                    //Console.WriteLine($"Adding quantity: {productAdddto.Quantity}");

                    exitproduct.Quantity += productAdddto.Quantity;
                    //Console.WriteLine($"New quantity (to save): {exitproduct.Quantity}");
                    await _productRepo.UpdateProduct(exitproduct);
                    return new Apiresponse<List<ProductAdddto>>
                    {
                        Data = new List<ProductAdddto> { productAdddto },
                        Message = "Product quantity updated successfully.",
                        Success = true,
                        Statuscode = 200
                    };

                }

                var categorycheck =await _productRepo.Categorycheck(productAdddto.CategoryId);
                if (!categorycheck)
                {
                    return new Apiresponse<List<ProductAdddto>>
                    {
                        Data = null,
                        Message = "Category does not exist.",
                        Statuscode = 400,

                    };
                }
                var prod = _mapper.Map<Product>(productAdddto);
                await _productRepo.Addproduct(prod);
                return new Apiresponse<List<ProductAdddto>>
                {
                    Data = new List<ProductAdddto> { productAdddto },
                    Message = "Product added successfully.",
                    Success = true,
                    Statuscode = 200
                };
            }catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
