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

        }
    }
}
