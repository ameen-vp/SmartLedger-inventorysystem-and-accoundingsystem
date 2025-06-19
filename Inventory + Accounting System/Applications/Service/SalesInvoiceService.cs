using Applications.ApiResponse;
using Applications.Dto;
using Applications.Interface;
using Domain.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Applications.Service
{
  public  class SalesInvoiceService : ISalesInvoiceService
    {
        private readonly ISalesInvoiceRepo _salesInvoiceRepo;
        private readonly IProductRepo _productRepo;
        private readonly IStockRepo _stockRepo;
        private readonly ICostmerRepo _costmerRepo;
        private readonly IStockTransactionsRepo _stockTransactionsRepo;

        public SalesInvoiceService(ISalesInvoiceRepo salesInvoiceRepo,IProductRepo productRepo, IStockRepo stockRepo, ICostmerRepo costmerRepo, IStockTransactionsRepo stockTransactionsRepo)
        {
            _salesInvoiceRepo = salesInvoiceRepo;
            _productRepo = productRepo;
            _stockRepo = stockRepo;
            _costmerRepo = costmerRepo;
            _stockTransactionsRepo = stockTransactionsRepo;
        }
        public async Task<Apiresponse<ADDSalesInvoice>> AddInvoice(ADDSalesInvoice salesInvoice)
        {
            try
            {
                var costomer = await _salesInvoiceRepo.GetCostomerId(salesInvoice.CustomerId);
                if (costomer == null)
                {
                    return new Apiresponse<ADDSalesInvoice>
                    {
                        Message = "Customer not found",
                        Statuscode = 404,
                        Data = null,
                        Success = false
                    };
                }
                var lastNumber = await _salesInvoiceRepo.GetLastInvoiceNumber();
                var newInvoiceNumber = "INV" + (lastNumber + 1).ToString("D4");
                var invoice = new SalesInvoice
                {
                    CustomerId = salesInvoice.CustomerId,
                    InvoiceDate = salesInvoice.InvoiceDate,
           
                    InvoiceNumber = newInvoiceNumber,
                    Remarks = salesInvoice.Remarks,

                    SalesItems = new List<SalesItems>(),
                };
                decimal invoiceamount = 0;
               

                foreach (var item in salesInvoice.SalesItems)
                {
                    var prod = await _productRepo.GetproductbyId(item.ProductId);
                    if(prod == null)
                    {
                        return new Apiresponse<ADDSalesInvoice>
                        {
                            Message = "Product not found",
                            Statuscode = 404,
                            Data = null,
                            Success = false
                        };
                    }
                    var stock = await _stockRepo.FindproductId(item.ProductId);
                    if(stock == null || stock.Quantity < item.Quantity)
                    {
                        return new Apiresponse<ADDSalesInvoice>
                        {
                            Statuscode = 404,
                            Message = $"Insufficient stock for Product ID {item.ProductId}. Available: {stock?.Quantity ?? 0}, Requested: {item.Quantity}",
                            Success = false,
                            Data = null
                        };
                    }
                    stock.Quantity -= item.Quantity;
                    await _stockRepo.UpdateStock(stock);

                    var txn = new StockTransactions
                    {
                        ProductId = item.ProductId,
                        Quantity = item.Quantity,
                        TransactionDate = DateTime.UtcNow,
                        TransactionType = Domain.Enum.Transactiontype.Sales,
                        StockId = stock.Id
                    };

                    await _stockTransactionsRepo.AddTransaction(txn);

                    var unitprice = prod.SellingPrice;
                    var gst = prod.SalesGst;
                    var quantity = item.Quantity;
                    var dis = item.Discount;


                    var subtotal = quantity * unitprice;
                    var discountamount = dis;
                    var afterdiscount = subtotal - discountamount;
                    var taxableamount = afterdiscount;
                    var gstamount = (taxableamount * gst) / 100m;
                    var total = gstamount + taxableamount;

                    invoice.SalesItems.Add(new SalesItems
                    {
                        ProductId = item.ProductId,
                        Quantity = quantity,
                        UNITPrice = unitprice,
                        Discount = discountamount,
                        Gst = gstamount,
                        TotalPrice = total
                    });
                    invoiceamount += total;
                }
                invoice.TotalAmount = invoiceamount;

                await _salesInvoiceRepo.AddSalesInvoice(invoice);
                return new Apiresponse<ADDSalesInvoice>
                {
                    Data = salesInvoice,
                    Message = "Sales invoice added successfully",
                    Statuscode = 200,
                    Success = true
                };

            }catch (Exception ex)
            {
                return new Apiresponse<ADDSalesInvoice>
                {
                    Message = ex.Message,
                    Statuscode = 500,
                    Data = null,
                    Success = false
                };
            }
        }
        public async Task<Apiresponse<List<SalesInvoicesDto>>> Get()
        {
            try
            {
                var invoice = await _salesInvoiceRepo.GetSalesInvoices();
                   if(invoice == null || invoice.Count == 0)
                { 
                    return new Apiresponse<List<SalesInvoicesDto>>
                    {
                        Message = "SalesInvoices Not found",
                        Statuscode = 404
                    };
                }
                var items = invoice.Select(x => new SalesInvoicesDto
                {
                    Id = x.Id,
                    CustomerId = x.CustomerId,
                    InvoiceDate = x.InvoiceDate,
                    InvoiceNumber = x.InvoiceNumber,
                    Status = x.Status,
                    Remarks = x.Remarks,
                    SalesItems = x.SalesItems.Select(x => new SalesItems
                    {
                        ProductId = x.ProductId,
                        UNITPrice = x.UNITPrice,
                        Quantity = x.Quantity,
                        Gst = x.Gst,
                        Discount = x.Discount,
                        TotalPrice = x.TotalPrice
                        ,SalesInvoice = x.SalesInvoice,
                        SalesInvoiceId =x.SalesInvoiceId,
                    }).ToList()

                }).ToList();
                return new Apiresponse<List<SalesInvoicesDto>>
                {
                    Message = "Invoice Fetched Sucsessfully",
                    Statuscode = 200,
                    Success = true,
                    Data = items
                };
                 
            }catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public async Task<Apiresponse<string>> UpdateSataus(UpdateStatusDto dto)
        {
            var check = await _salesInvoiceRepo.GetInvoiceId(dto.InvoiceId);
            if(check == null)
            {
                return new Apiresponse<string>
                {
                    Message = "Invoice Id not Found"
                    ,
                    Statuscode = 404,
                    Success = false,
                    Data = null
                };
            }
            if(dto.InvoiceStatus == Domain.Enum.InvoiceStatus.PENDING 
        }
    }
}
