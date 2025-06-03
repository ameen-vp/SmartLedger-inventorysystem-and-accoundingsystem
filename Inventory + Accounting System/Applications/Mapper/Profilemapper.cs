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


        }
    }
}
