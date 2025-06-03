using Applications.ApiResponse;
using Applications.Dto;
using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Applications.Interface
{
  public  interface Iauthservice
    {
        Task<Apiresponse<string>> Register(UserregisterDto userregisterDto);

        Task<Apiresponse<UserResponsedto>> Loginuser(Logindto logindto);

        Task<Apiresponse<string>> Deleteuser(string name);

        Task<List<User>> Getusers();
    }
}
