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
using System.Xml.Schema;

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
                    

                    exitproduct.Quantity += productAdddto.Quantity;
                 
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
        public async Task<Apiresponse<List<Productviewdto>>> GetAllproducts()
        {
            try
            {
                var prod = await _productRepo.GetAllProducts();
                if (prod == null)
                {
                    return new Apiresponse<List<Productviewdto>>
                    {
                        Data = null,
                        Message = "No products found.",
                        Success = false,
                        Statuscode = 404
                    };
                }
                var productDtos = prod.Select(x => new Productviewdto
                {
                    Id = x.Id,
                    ProductName = x.ProductName,
                    SKU = x.SKU,
                    Quantity = x.Quantity,
                    CategoryName = x.category?.CategoryName,
                    SellingPrice = x.SellingPrice


                }).ToList();
                return new Apiresponse<List<Productviewdto>>
                {
                    Data = productDtos,
                    Message = "Products Fetched successfully.",
                    Success = true,
                    Statuscode = 200
                };
            }catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
       public async Task<Apiresponse<Productviewdto>> Getproductbyid(int id)
        {
            try
            {
                var idcheck = await _productRepo.productexit(id);
                if (!idcheck)
                {
                    return new Apiresponse<Productviewdto>
                    {
                        Data = null,
                        Message = "product not found",
                        Statuscode = 404,
                        Success = false
                    };
                }
                var prod = await _productRepo.GetproductbyId(id);

                var dto = new Productviewdto

                {
                    Id = prod.Id,
                    //ProductName = prod.ProductName,
                    CategoryName = prod.category.CategoryName,
                    ProductName = prod.ProductName,
                    SKU = prod.SKU,
                    SellingPrice = prod.SellingPrice,
                    Quantity = prod.Quantity

                };
                return new Apiresponse<Productviewdto>
                {
                    Data = dto,
                    Message = "product fetched sucessfully",
                    Success = true,
                    Statuscode = 200
                };
            }catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }   
        }
        public async Task<Apiresponse<productupdatedto>> Updateproduct(int id,productupdatedto productupdatedto)
        {
            try
            {
                var check = await _productRepo.GetproductbyId(id);

                if (check == null)
                {
                    return new Apiresponse<productupdatedto>
                    {
                        Data = null,
                        Message = "Product not found",
                        Statuscode = 404,
                        Success = false
                    };
                }
                var catcheck = await _productRepo.Categorycheck(productupdatedto.categoryId);
                if (!catcheck)
                {
                    {
                        return new Apiresponse<productupdatedto>
                        {
                            Data = null,
                            Message = "Invalid Category ID.",
                            Statuscode = 400,
                            Success = false
                        };
                    }
                }
                var product = _mapper.Map<Product>(productupdatedto);

                product.Id = id;

                await _productRepo.UpdateProduct(product);
                return new Apiresponse<productupdatedto>
                {
                    Data = productupdatedto,
                    Message = "Product updated sucessfully",
                    Success = true,
                    Statuscode = 200
                };
            }catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }
        public async Task<Apiresponse<productupdatedto>> Updateproduct(int id, productupdatedto productupdatedto)
        {
            var product = await _productRepo.GetproductbyId(id);

            if (product == null)
            {
                return new Apiresponse<productupdatedto>
                {
                    Data = null,
                    Message = "Product not found",
                    Statuscode = 404,
                    Success = false
                };
            }

            var catcheck = await _productRepo.Categorycheck(productupdatedto.categoryId);
            if (!catcheck)
            {
                return new Apiresponse<productupdatedto>
                {
                    Data = null,
                    Message = "Invalid Category ID.",
                    Statuscode = 400,
                    Success = false
                };
            }

            product.SellingPrice = productupdatedto.Sellingprice;
            product.Quantity = productupdatedto.Quantity;
            product.CategoryId = productupdatedto.categoryId;

            await _productRepo.UpdateProduct(product);

            return new Apiresponse<productupdatedto>
            {
                Data = productupdatedto,
                Message = "Product updated successfully",
                Statuscode = 200,
                Success = true
            };
        }


    }
}
