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

        private async Task<Product> Findproducts(int id)
        {
            var product = await _productRepo.GetproductbyId(id);
            if (product == null)
            {
                throw new Exception("Product not found");
            }
            return product;
        }
      public async  Task<Apiresponse<List<ProductAdddto>>> AddProduct(ProductAdddto productAdddto)
        {
            try
            {
                var exit =await _productRepo.Exits(productAdddto.ProductName);
               

                if (exit)
                {
                    return new Apiresponse<List<ProductAdddto>>
                    {
                        Data = new List<ProductAdddto> { productAdddto },
                        Message = "Product Already Exits",
                        Success = false,
                        Statuscode = 400
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

                    SupplierId = x.SupplierId,
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
            
                var prod = await Findproducts(id);
               
             

                var dto = new Productviewdto

                {
                    Id = prod.Id,
                    ProductName = prod.ProductName,
                   SupplierId = prod.SupplierId,
                    SKU = prod.SKU,
                    SellingPrice = prod.SellingPrice,

                };
                return new Apiresponse<Productviewdto>
                {
                    Data = dto,
                    Message = "product fetched sucessfully",
                    Success = true,
                    Statuscode = 200
                };
            
        }
       
        public async Task<Apiresponse<productupdatedto>> Updateproducts(int id, productupdatedto productupdatedto)
        {
            var product = await Findproducts(id);

           

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
            product.CategoryId= productupdatedto.categoryId;

            await _productRepo.UpdateProduct(product);

            return new Apiresponse<productupdatedto>
            {
                Data = productupdatedto,
                Message = "Product updated successfully",
                Statuscode = 200,
                Success = true
            };
        }
        public async Task<Apiresponse<string>> Deleteproduct(int id)
        {
            var prod = await _productRepo.DeleteProducts(id);
            if (!prod)
            {
                return new Apiresponse<string>
                {
                    Data = null,
                    Message = "Product not found or could not be deleted.",
                    Statuscode = 404,
                    Success = false
                };
            }

            return new Apiresponse<string>
            {
                Data = null,
                Message = "Deleted Sucessfully",
                Statuscode = 200,
                Success = true
            };


        }
        // Service method (inside ProductService)
        public async Task<List<Productviewdto>> GetAllProductsAsync()
        {
            var products = await _productRepo.GetProductviewdtos();

            var productDtos = products.Select(p => new Productviewdto
            {
                Id = p.Id,
                ProductName = p.ProductName,
                StockTransactionViewDtos = p.StockTransactions.Select(st => new StockTransactionViewDto
                {
                    Id = st.Id,
                    ProductId = st.ProductId,
                    TransactionDate = st.TransactionDate,
                    Quantity = st.Quantity
                    , TransactionType = st.TransactionType,
                    StockId = st.StockId
                }).ToList()
            }).ToList();

            return productDtos;
        }

    }
}
