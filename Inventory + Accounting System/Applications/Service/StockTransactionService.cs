
using Applications.ApiResponse;
using Applications.Dto;
using Applications.Interface;
using AutoMapper;
using Domain.Models;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace Applications.Service
{
    public class StockTransactionService : IStockTransactionServices
    {
        private readonly IStockTransactionsRepo _stockTransactionsRepo;
        private readonly IMapper _mapper;
        private readonly IStockRepo _stockRepo;
        public StockTransactionService(IStockTransactionsRepo stockTransactionsRepo, IMapper mapper, IStockRepo stockRepo)
        {
            _stockTransactionsRepo = stockTransactionsRepo;
            _mapper = mapper;
            _stockRepo = stockRepo;
        }

        public async Task<Apiresponse<AddStockTransactionDto>> AddTransactions(AddStockTransactionDto addStockTransactionDto)
        {
            try
            {
                var stock = await _stockRepo.GetstockId(addStockTransactionDto.ProductId);
                if (stock == null)
                {
                    stock = new Stocks
                    {
                        ProductId = addStockTransactionDto.ProductId,
                        Quantity = 0,
                        Date = DateTime.Now
                    };
                  await  _stockRepo.Addstock(stock);
                    
                }
                var transaction = _mapper.Map<StockTransactions>(addStockTransactionDto);

                transaction.StockId = stock.Id;


                if (transaction.TransactionType == Domain.Enum.Transactiontype.Purchase ||
                    transaction.TransactionType == Domain.Enum.Transactiontype.Return)
                {
                    stock.Quantity += transaction.Quantity;
                }
                else if (transaction.TransactionType == Domain.Enum.Transactiontype.Sales ||
                         transaction.TransactionType == Domain.Enum.Transactiontype.Damage)
                {
                    if (stock.Quantity < transaction.Quantity)
                    {
                        return new Apiresponse<AddStockTransactionDto>
                        {
                            Message = "Insufficient stock to process this transaction.",
                            Statuscode = 400,
                            Success = false,
                            Data = null
                        };
                    }
                    stock.Quantity -= transaction.Quantity;
                }
                else
                {
                    return new Apiresponse<AddStockTransactionDto>
                    {
                        Message = "Invalid transaction type.",
                        Statuscode = 400,
                        Success = false,
                        Data = null
                    };
                }
                stock.LastUpdated = DateTime.UtcNow;

                await _stockTransactionsRepo.AddTransaction(transaction);
                await _stockRepo.UpdateStock(stock);
                return new Apiresponse<AddStockTransactionDto>
                {
                    Statuscode = 200,
                    Message = "Transaction sucessfully",
                    Success = true

                };
            }
            catch (Exception ex)
            {
                return new Apiresponse<AddStockTransactionDto>
                {
                    Message = ex.Message,
                    Data = null,
                    Success = false,
                    Statuscode = 500
                };
}
        }
        public async Task<Apiresponse<List<StockTransactionViewDto>>> Get()
        {
            try
            {
                var get = await _stockTransactionsRepo.Gettransactions();
                if(get == null)
                {
                    return new Apiresponse<List<StockTransactionViewDto>>
                    {   
                        Message = "Transactions not Found",
                        Statuscode = 400
                    };
                }
                var transactions = get.Select(x => new StockTransactionViewDto
                {
                    Id = x.Id,
                    ProductId = x.ProductId,
                    Quantity = x.Quantity,
                    TransactionDate = x.TransactionDate,
                    TransactionType = x.TransactionType,
                    StockId = x.StockId
                    //,Productname = x.Productname
                }).ToList();

                return new Apiresponse<List<StockTransactionViewDto>>
                
                {  Data = transactions,
                    Message = "Transaction Fetched SucsessFully",
                    Statuscode = 200,
                };
            }catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
          
        }
       public async Task<Apiresponse<string>> Delete(int id)
        {
            var del = await _stockTransactionsRepo.DeleteTransaction(id);
            if(del)
            {
                return new Apiresponse<string>
                {
                    Statuscode = 200,
                    Message = "Deleted Sucsessfully",
                    Success = true
                };
            }
            return new Apiresponse<string>
            {
                Message = "TransactionId Not Found",
                Statuscode = 404,
                Success = false
            };
        }
    }
}
