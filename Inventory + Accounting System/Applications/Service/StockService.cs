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
   public  class StockService : IStockService
    {
        private readonly IStockRepo _stockRepo;
        private readonly IMapper _mapper;

        public StockService(IStockRepo stockRepo,IMapper mapper)
        {
            _stockRepo = stockRepo;
            _mapper = mapper;
        }
       
       public async Task<Apiresponse<StockAdddto>> Addstock(StockAdddto stockAdddto)
        {
            try
            {

                var check = await _stockRepo.GetByProductId(stockAdddto.ProductId);
                if (check != null)
                {
                    return new Apiresponse<StockAdddto>
                    {
                        Data = null,
                        Statuscode = 400,
                        Message = "Stock already exists for this ProductId",
                        Success = false
                    };
                }
                var stock = new Stocks
                {
                    ProductId = stockAdddto.ProductId,
                    Quantity = stockAdddto.Quantity

                };
                await _stockRepo.Addstock(stock);
                return new Apiresponse<StockAdddto>
                {
                    Message = "Stock addes",
                    Data = stockAdddto,
                    Statuscode = 200,
                    Success = true
                };
            }catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }
        public async Task<Apiresponse<List<StockviewDto>>> Get()
        {
            var get = await _stockRepo.Getstock();
            if(get == null || get.Count == 0)
            {
                return new Apiresponse<List<StockviewDto>>
                {
                    Data = null,
                    Message = "Stock not avialable",
                    Statuscode = 400,
                    Success = false
                };
            }
            var map = _mapper.Map<List<StockviewDto>>(get);
            return new Apiresponse<List<StockviewDto>>
            {
                Data = map ,
                Message = "Stocks fetched Sucessessfully",
                Success = true,
                Statuscode = 200
            };
        }
        public async Task<Apiresponse<StockviewDto>> GetById(int id)
        {
            var check = await _stockRepo.FindId(id);
            if(check == null)
            {
                return new Apiresponse<StockviewDto>
                {
                    Statuscode = 404,
                    Message = "Product not foun",
                    Success = false,
                    Data = null
                };
            }
            var map = _mapper.Map<StockviewDto>(check);
            return new Apiresponse<StockviewDto>
            {
                Statuscode = 200,
                Data = map,
                Success = true,
                Message = "stockItems Fetched SucssessFully"

            };
        }
        public async Task<Apiresponse<string>> Delete(int id)
        {
            var del = await _stockRepo.DeleteStock(id);
            if (!del)
            {
                return new Apiresponse<string>
                {
                    Message = "Stock not found",
                    Data = null,
                    Success = false,
                    Statuscode = 404
                };
            }
            return new Apiresponse<string>
            {
                Message = "Stock deleted successfully",
                Data = null,
                Success = true,
                Statuscode = 200
            };
        }
    }
}
