using Applications.Dto;
using AutoMapper;
using Domain.Models;


namespace Applications.Mapper
{
   public class Profilemapper : Profile
    {
       public Profilemapper()
        {
          CreateMap<UserregisterDto,User>().ReverseMap();
            CreateMap<User, Logindto>().ReverseMap();
            CreateMap<User, UserResponsedto>().ReverseMap();
            CreateMap <Category,CategoryAdddto>().ReverseMap();
            CreateMap <Product, ProductAdddto>().ReverseMap();
            CreateMap<Product, Productviewdto>().ReverseMap();
            CreateMap<productupdatedto, Product>().ReverseMap();
            CreateMap<CostomerDto, Costomer>().ReverseMap();
            CreateMap<VendorAdddto, Vendor>().ReverseMap();
            CreateMap<Stocks,StockAdddto>().ReverseMap();
            CreateMap<StockviewDto, Stocks>().ReverseMap();
            CreateMap<Stockupdatedto,Stocks>().ReverseMap();
            CreateMap<AddStockTransactionDto, StockTransactions>().ReverseMap();
            CreateMap<StockTransactions, StockTransactionViewDto>().ReverseMap();
            CreateMap<AddPurchaseinvoiceDto, PurchaseInvoice>().ReverseMap();
            CreateMap<PurchaseItemCreateDto, PurchaseItems>().ReverseMap();
            CreateMap<PurchaseinvoiceDto, PurchaseInvoice>().ReverseMap();
            CreateMap<PurchaseItemDto, PurchaseItems>().ReverseMap();
            CreateMap<SalesInvoicesDto, SalesInvoice>().ReverseMap();
            CreateMap<AddSalesItemDto, SalesItems>().ReverseMap();
            CreateMap<ADDSalesInvoice,SalesInvoice>().ReverseMap();
            CreateMap<UpdateStatusDto, SalesInvoice>().ReverseMap();
            CreateMap<AddaccountDto, Accounts>().ReverseMap();
            CreateMap<AddLedgerDto, LedgerEntry>().ReverseMap();
        }
    }
}
