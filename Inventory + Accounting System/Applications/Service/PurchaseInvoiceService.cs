
using Applications.ApiResponse;
using Applications.Dto;
using Applications.Interface;
using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Applications.Service
{
    public class PurchaseInvoiceService : Ipurchaseinvoiceservice
    {
        private readonly IpurchaseInvoiceRepo _ipurchaseInvoiceRepo;
        private readonly IProductRepo _productRepo;
        private readonly IStockRepo _stockRepo;
        private readonly IStockTransactionsRepo _stockTransactionsRepo;
        private readonly IStockTransactionServices _stockTransactionServices;
        public PurchaseInvoiceService(IpurchaseInvoiceRepo ipurchaseInvoiceRepo, IProductRepo productRepo, IStockRepo stockRepo, IStockTransactionsRepo stockTransactionsRepo, IStockTransactionServices stockTransactionServices)
        {
            _ipurchaseInvoiceRepo = ipurchaseInvoiceRepo;
            _productRepo = productRepo;
            _stockRepo = stockRepo;
            _stockTransactionsRepo = stockTransactionsRepo;
            _stockTransactionServices = stockTransactionServices;
        }
        public async Task<Apiresponse<string>> AddInvoices(AddPurchaseinvoiceDto addPurchaseinvoiceDto)
        {
            try
            {
                var vendor = await _ipurchaseInvoiceRepo.GetvendorsId(addPurchaseinvoiceDto.VendorId);

                if (vendor == null)
                {
                    return new Apiresponse<string>
                    {
                        Data = null,
                        Message = "Vendors not found",
                        Statuscode = 404,
                        Success = false
                    };
                }
                var purchaseinfo = new PurchaseInvoice
                {
                    VentorsId = addPurchaseinvoiceDto.VendorId,
                    Date = addPurchaseinvoiceDto.InvoiceDate,
                    InvoiceNumber = "INV-" + Guid.NewGuid().ToString().Substring(0, 8),
                    purchaseItems = new List<PurchaseItems>()
                };
                
                   decimal withgst = 0;
                decimal withoutgst = 0;

                foreach (var item in addPurchaseinvoiceDto.PurchaseItems)
                {
                    var product = await _productRepo.GetproductbyId(item.ProductId);
                    if (product == null)
                    {
                        return new Apiresponse<string>
                        {
                            Statuscode = 494,
                            Message = "Product not found"
                        };
                    }

                    var totalamount = item.UnitPrice * item.Quantity;
                    var gst = (item.GSTPercent / 100m) * totalamount;
                    var itemtotal = totalamount + gst;


                    withgst += itemtotal;
                    withoutgst += totalamount;

                    var items = new PurchaseItems
                    {
                        ProductId = item.ProductId,
                        //ProductName = product.ProductName,
                        Quantity = item.Quantity,
                        UnitPrice = item.UnitPrice,
                        GSTPercentage = item.GSTPercent,
                        GSTAmount = gst,
                        ToTalAmount = itemtotal
                    };
                    purchaseinfo.purchaseItems.Add(items);
                    var exitingstock = await _stockRepo.FindproductId(item.ProductId);
                    int stokId = 0;
                    if(exitingstock != null)
                    {
                        exitingstock.Quantity += item.Quantity;
                        await _stockRepo.UpdateStock(exitingstock);
                        stokId = exitingstock.Id;
                    }
                    else
                    {
                        var newstock = new Stocks
                        {
                            ProductId = item.ProductId,
                            Quantity = item.Quantity,
                            
                        };
                        await _stockRepo.Addstock(newstock);
                        stokId = newstock.Id;
                    }
                    var txn = new AddStockTransactionDto
                    {
                        ProductId = item.ProductId,
                        Quantity = item.Quantity,
                        TransactionDate = DateTime.UtcNow,
                        TransactionType = Domain.Enum.Transactiontype.Purchase,
                    
                    };
                    await _stockTransactionServices.AddTransactions(txn);
                }
                purchaseinfo.TotalAmount = withoutgst;
                purchaseinfo.GrantToTal = withgst;
                purchaseinfo.GST = withgst - withoutgst;

                await _ipurchaseInvoiceRepo.AddInvoice(purchaseinfo);
              

                return new Apiresponse<string>
                {
                    Data = null,
                    Message = "Purchase invoice added successfully",
                    Success = true,
                    Statuscode = 200
                };

            }catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public async Task<Apiresponse<List<PurchaseinvoiceDto>>> GetInvoice()
        {
            try
            {
                var get = await _ipurchaseInvoiceRepo.GetInvoice();
                if(get == null || get.Count== 0)
                {
                    return new Apiresponse<List<PurchaseinvoiceDto>>
                    {
                        Data = null,
                        Message = "purchase invoice not found",
                        Success = false,
                        Statuscode = 404
                    };
                }
                var purchaseinfo = get.Select(x => new PurchaseinvoiceDto
                {
                    Id = x.Id,
                    VentorsId = x.VentorsId,
                    InvoiceNumber = x.InvoiceNumber,
                    Date = x.Date,
                    TotalAmount = x.TotalAmount,
                    GST = x.GST,
                    GrantToTal = x.GrantToTal,
                    purchaseItem = x.purchaseItems.Select(y => new PurchaseItemDto
                    {
                        ProductId = y.ProductId,
                        Quantity = y.Quantity,
                        UnitPrice = y.UnitPrice,
                        GSTPercentage = y.GSTPercentage,
                        //GSTAmount = y.GSTAmount,
                        //ToTalAmount = y.ToTalAmount
                        

                    }).ToList()

                }).ToList();
                return new Apiresponse<List<PurchaseinvoiceDto>>
                {
                    Data = purchaseinfo,
                    Message = "Invoice Fetched Sucsessfully",
                    Statuscode = 200,
                    Success = true
                };
            }catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public async Task<Apiresponse<string>> Delete(int id)
        {
            var del = await _ipurchaseInvoiceRepo.Deleteinvoice(id);
            if(!del)
            {
                return new Apiresponse<string>
                {
                    Message = "invoice not found",
                    Statuscode = 404,
                    Success = false,

                };
            }
            return new Apiresponse<string>
            {
                Message = "Deleted sucsessfully",
                Success = true,
                Statuscode = 200
            };
        }
        public async Task<Apiresponse<PurchaseinvoiceDto>> GetInvoiceById(int id)
        {
            try
            {
                var invoice = await _ipurchaseInvoiceRepo.GetInvoiceById(id);
                if (invoice == null)
                {
                    return new Apiresponse<PurchaseinvoiceDto>
                    {
                        Data = null,
                        Message = "Invoice not found",
                        Success = false,
                        Statuscode = 404

                    };
                }
                var invoices = new PurchaseinvoiceDto
                {
                    Id = invoice.Id,
                    VentorsId = invoice.VentorsId,
                    InvoiceNumber = invoice.InvoiceNumber,
                    Date = invoice.Date,
                    TotalAmount = invoice.TotalAmount,
                    GST = invoice.GST,
                    GrantToTal = invoice.GrantToTal,
                    purchaseItem = invoice.purchaseItems.Select(y => new PurchaseItemDto
                    {
                        ProductId = y.ProductId,
                        Quantity = y.Quantity,
                        UnitPrice = y.UnitPrice,
                        GSTPercentage = y.GSTPercentage,

                    }).ToList()
                };
                return new Apiresponse<PurchaseinvoiceDto>
                {
                    Data = invoices,
                    Message = "Invoices Fetches sucsessfully"
                    , Statuscode = 200,
                    Success = false
                };
            }catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
       
    }
}